using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics; // Add the System.Diagnostics namespace

public class ZipHandler
{
    private static readonly HttpClient client = new HttpClient(); // Use a singleton HttpClient for better performance

    public async Task DownloadExtractZip(string sUrl, string sZipName, string sExtractPath)
    {
        try
        {
            await DownloadZip(sUrl, sZipName);
            Debug.WriteLine("File downloaded successfully.");
            ExtractZip(sZipName, sExtractPath);
            Debug.WriteLine("File extracted successfully.");
        }
        catch (HttpRequestException httpEx)
        {
            Debug.WriteLine($"Error downloading file: {httpEx.Message}");
            throw;
        }
        catch (FileNotFoundException fileEx)
        {
            Debug.WriteLine($"File not found: {fileEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }
    }

    private async Task DownloadZip(string sUrl, string sZipName)
    {
        try
        {
            HttpResponseMessage res = await client.GetAsync(sUrl);
            res.EnsureSuccessStatusCode();
            byte[] bFile = await res.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(sZipName, bFile);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error downloading zip file: {ex.Message}");
            throw;
        }
    }

    private void ExtractZip(string sZipName, string sExtractPath)
    {
        if (!File.Exists(sZipName))
        {
            throw new FileNotFoundException($"Zip file '{sZipName}' not found.");
        }

        try
        {
            // Ensure extraction directory exists, create it if necessary
            if (!Directory.Exists(sExtractPath))
            {
                Directory.CreateDirectory(sExtractPath);
            }

            ZipFile.ExtractToDirectory(sZipName, sExtractPath);
            Debug.WriteLine($"Extracted contents to {sExtractPath}");

            // Delete the zip file after extraction
            File.Delete(sZipName);
            Debug.WriteLine($"Deleted the zip file: {sZipName}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error extracting zip file: {ex.Message}");
            throw;
        }
    }
}
