using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TruekAppAPI.Services
{
    public interface IStorageService
    {
        /// <summary>
        /// Sube un archivo a un contenedor de Azure Blob Storage.
        /// </summary>
        /// <param name="file">El IFormFile recibido del controlador.</param>
        /// <param name="containerName">El nombre del contenedor (ej: "listings", "profiles").</param>
        /// <returns>La URL pública y completa del archivo subido.</returns>
        Task<string> UploadFileAsync(IFormFile file, string containerName);

        /// <summary>
        /// Elimina un archivo de Azure Blob Storage usando su URL completa.
        /// </summary>
        /// <param name="fileUrl">La URL completa del blob a eliminar.</param>
        Task DeleteFileAsync(string fileUrl);
    }
}