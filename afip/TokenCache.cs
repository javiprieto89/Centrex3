using System;
using System.Collections.Generic;
using System.IO;

namespace Centrex.Afip
{
    /// <summary>
    /// Cache de tickets AFIP: archivo por clave (servicio|modo).
    /// Sin dependencias JSON (ideal .NET Framework).
    /// </summary>
    public sealed class TokenCache
    {
        private static readonly string BaseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "afip_cache");

        private static readonly Dictionary<string, AfipTicket> MemoryCache = new Dictionary<string, AfipTicket>();

        private TokenCache()
        {
        }

        private static string BuildKey(string serviceName, AfipMode mode)
        {
            return $"{serviceName.ToLower()}|{mode.ToString().ToLower()}";
        }

        private static string BuildFilePath(string key)
        {
            string safeName = key.Replace("|", "_");
            return Path.Combine(BaseFolder, $"{safeName}.ta");
        }

        public static void Save(string serviceName, AfipMode mode, AfipTicket ticket)
        {
            try
            {
                string key = BuildKey(serviceName, mode);
                MemoryCache[key] = ticket;

                if (!Directory.Exists(BaseFolder))
                {
                    Directory.CreateDirectory(BaseFolder);
                }

                string path = BuildFilePath(key);
                string[] lines = new[] { ticket.Token, ticket.Sign, ticket.Expiration.ToString("o") };
                File.WriteAllLines(path, lines);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"[TokenCache] Error al guardar ticket: {ex.Message}");
            }
        }

        public static AfipTicket GetTicket(string serviceName, AfipMode mode)
        {
            try
            {
                string key = BuildKey(serviceName, mode);
                if (MemoryCache.ContainsKey(key))
                {
                    return MemoryCache[key];
                }

                string path = BuildFilePath(key);
                if (!File.Exists(path))
                    return null;

                string[] lines = File.ReadAllLines(path);
                if (lines.Length < 3)
                    return null;

                var t = new AfipTicket()
                {
                    Token = lines[0],
                    Sign = lines[1],
                    Expiration = DateTime.Parse(lines[2])
                };
                MemoryCache[key] = t;
                return t;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"[TokenCache] Error al obtener ticket: {ex.Message}");
                return null;
            }
        }

        public static void ClearAll()
        {
            try
            {
                MemoryCache.Clear();
                if (Directory.Exists(BaseFolder))
                {
                    Directory.Delete(BaseFolder, recursive: true);
                }
                Console.WriteLine("[TokenCache] Cache limpia.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TokenCache] Error al limpiar cache: {ex.Message}");
            }
        }
    }
}
