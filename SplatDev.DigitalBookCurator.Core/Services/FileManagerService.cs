using Microsoft.Extensions.Logging;

using SplatDev.DigitalBookCurator.Core.Constants;
using SplatDev.DigitalBookCurator.Core.Extensions;
using SplatDev.DigitalBookCurator.Core.Models;
using SplatDev.DigitalBookCurator.Core.Readers;
using SplatDev.DigitalBookCurator.Core.Repositories;

namespace SplatDev.DigitalBookCurator.Core.Services
{
    public class FileManagerService
    {
        private readonly IBookRepository bookRepository;
        private readonly ILogger<FileManagerService> logger;
        private static int count = 0;

        public FileManagerService(IBookRepository bookRepository, ILogger<FileManagerService> logger)
        {
            this.bookRepository = bookRepository;
            this.logger = logger;
        }

        public async Task OrganizeFiles(string path, string destination = "", string extension = FileExtensions.PDF)
        {
            var intro = "Introduction";
            OnFolderTraverse?.Invoke(this, path);

            var directory = new DirectoryInfo(path);
            if (directory is null) return;
            var allFiles = directory.GetFiles(extension, SearchOption.TopDirectoryOnly);
            foreach (var file in allFiles)
            {
                var book = ProcessBookFile(file.FullName, extension);
                if (book is not null)
                {
                    var existingBook = await bookRepository.GetBookByTitleAsync(book.Title);
                    if (existingBook != null)
                    {
                        logger.LogError("Book already exists: {title}", book.Title);
                        continue;
                    }
                    var bookName = !string.IsNullOrEmpty(book.Title) ? book.Title : file.Name.CleanupFileName();
                    var bookNameNormalized = bookName.Contains(intro) ? bookName[intro.Length..] : bookName;
                    if (bookNameNormalized.Length <= 10 || bookName.Contains("Untitled", StringComparison.InvariantCultureIgnoreCase))
                        bookName = new FileInfo(file.FullName).Name.CleanupFileName();

                    book.FileName = $"{bookName}{extension[1..]}";
                    if (new FileInfo(Path.Combine(destination, book.FileName)).Exists)
                    {
                        logger.LogError("File already exists: {filename}", book.FileName);
                        book.FileName = file.Name.CleanupFileName();
                        if (book.Title == PdfReader.GetBookTitle(file.FullName))
                        {
                            logger.LogError("Book already exists: {title}", book.Title);
                            continue;
                        }
                    }
                    try
                    {
                        if (string.IsNullOrEmpty(book.Title)) book.Title = bookName;

                        await bookRepository.AddBookAsync(book);

                        File.Move(file.FullName, Path.Combine(destination, book.FileName));
                        count++;
                        OnBookCountChanged?.Invoke(this, count);
                        OnBookAdded?.Invoke(this, book.Title);
                    }
                    catch (Exception ex)
                    {
                        // if could not add book to db, don't move it!
                        logger.LogError("Error: {filename} | {error}", book.FileName, ex.Message);
                    }
                }
            }

            var subDirectories = directory.GetDirectories("*", SearchOption.AllDirectories);
            if (subDirectories.Length > 0)
            {
                foreach (var folder in subDirectories)
                    await OrganizeFiles(folder.FullName, destination, extension);
            }

        }

        public async Task DeleteEmptyFolders(string rootPath)
        {
            count = 0;
            await Task.FromResult(0);
            DeleteFolder(rootPath);
        }

        private void DeleteFolder(string path)
        {
            var directory = new DirectoryInfo(path);
            OnFolderTraverse?.Invoke(this, path);

            if (directory is null) return;

            try
            {
                var allDirectories = directory.GetDirectories("*", SearchOption.AllDirectories);

                foreach (var folder in allDirectories)
                    DeleteFolder(folder.FullName);

                if (!directory.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Any())
                {
                    directory.Delete();
                    count++;
                    OnFolderCountChanged?.Invoke(this, count);
                    OnFolderDeleted?.Invoke(this, directory.FullName);
                }
            }
            catch { }
        }

        private Book? ProcessBookFile(string path, string extension)
        {
            return extension switch
            {
                _ => new PdfReader().ReadPdf(logger, path),
            };
        }

        public void DeleteBook(int id)
        {
            OnBookDeleted?.Invoke(OnBookDeleted, id.ToString());
        }

        #region Events
        public event EventHandler<string>? OnBookAdded;
        public event EventHandler<string>? OnBookDeleted;
        public event EventHandler<string>? OnFolderDeleted;
        public event EventHandler<int>? OnBookCountChanged;
        public event EventHandler<int>? OnFolderCountChanged;
        public event EventHandler<string>? OnFolderTraverse;
        #endregion
    }
}
