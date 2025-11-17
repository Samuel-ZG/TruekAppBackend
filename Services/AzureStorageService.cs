using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration; // Para IConfiguration
using System;
using System.IO;
using System.Threading.Tasks;

namespace TruekAppAPI.Services
{
    public class AzureStorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _connectionString;

        // Inyectamos IConfiguration para leer el appsettings.json
        public AzureStorageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("AzureStorage:ConnectionString").Value;
            
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("La cadena de conexión de Azure Storage no está configurada.");
            }
            
            _blobServiceClient = new BlobServiceClient(_connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            // 1. Obtener/Crear el contenedor
            // Práctica profesional: Usar minúsculas para nombres de contenedores.
            containerName = containerName.ToLower(); 
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            
            // Asegurarse de que el contenedor exista y sea público
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // 2. Generar un nombre de archivo único
            // Convención: Usar un GUID para evitar colisiones de nombres.
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            
            // 3. Obtener una referencia al blob
            var blobClient = containerClient.GetBlobClient(uniqueFileName);

            // 4. Subir el archivo (stream)
            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            // 5. Devolver la URL pública del archivo
            return blobClient.Uri.ToString();
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;

            try
            {
                var fileUri = new Uri(fileUrl);
                // La URL tiene el formato: https://[storage_account].blob.core.windows.net/[container_name]/[blob_name]
                
                // Segment[0] es "/"
                var containerName = fileUri.Segments[1].TrimEnd('/');
                var blobName = fileUri.Segments[2];

                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(blobName);

                // Eliminar el blob si existe
                await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                // Es importante registrar (log) este error, pero no relanzar la excepción
                // si no queremos que falle toda la operación (ej: un Delete en el controlador).
                Console.WriteLine($"Error al eliminar el blob: {ex.Message}");
            }
        }
    }
}