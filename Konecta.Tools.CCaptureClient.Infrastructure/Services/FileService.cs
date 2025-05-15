using Konecta.Tools.CCaptureClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // Required for File operations

namespace Konecta.Tools.CCaptureClient.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public string ReadFileAsBase64(string filePath)
        {
            // Validate file exists
            if (!File.Exists(filePath))
            {
                LoggerHelper.LogError($"File not found: {filePath}"); // Log error
                throw new FileNotFoundException($"The file was not found: {filePath}");
            }

            try
            {
                LoggerHelper.LogInfo($"Reading file as Base64: {filePath}"); // Log file read attempt
                byte[] fileBytes = File.ReadAllBytes(filePath);
                var result = Convert.ToBase64String(fileBytes);
                LoggerHelper.LogInfo($"Successfully read file as Base64: {filePath}"); // Log successful read
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Error reading file: {filePath}", ex); // Log error
                throw new IOException($"Error reading file: {ex.Message}", ex);
            }
        }

        public string GetFileName(string filePath)
        {
            try
            {
                LoggerHelper.LogInfo($"Getting file name from path: {filePath}"); // Log file name retrieval attempt
                var fileName = Path.GetFileName(filePath);
                LoggerHelper.LogInfo($"Retrieved file name: {fileName}"); // Log successful retrieval
                return fileName;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Invalid file path: {filePath}", ex); // Log error
                throw new ArgumentException($"Invalid file path: {ex.Message}", ex);
            }
        }
    }
}