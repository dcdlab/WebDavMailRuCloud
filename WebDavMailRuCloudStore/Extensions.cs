﻿using System;
using System.Text;
using System.Threading.Tasks;
using MailRuCloudApi;
using NWebDav.Server.Http;
using NWebDav.Server.Stores;
using YaR.WebDavMailRu.CloudStore.Mailru.StoreBase;

namespace YaR.WebDavMailRu.CloudStore
{
    internal static class Extensions
    {
        public static Task<bool> Remove(this MailRuCloud cloud, IStoreItem item)
        {
            if (null == item) return Task.FromResult(false);

            var storeItem = item as MailruStoreItem;
            if (storeItem != null)
                return cloud.Remove(storeItem.FileInfo);
            var storeCollection = item as MailruStoreCollection;
            if (storeCollection != null)
                return cloud.Remove(storeCollection.DirectoryInfo);

            throw new ArgumentException(string.Empty, nameof(item));
        }

        public static Task<bool> Rename(this MailRuCloud cloud, IStoreItem item, string destinationName)
        {
            if (null == item) return Task.FromResult(false);

            var storeItem = item as MailruStoreItem;
            if (storeItem != null)
                return cloud.Rename(storeItem.FileInfo, destinationName);
            var storeCollection = item as MailruStoreCollection;
            if (storeCollection != null)
                return cloud.Rename(storeCollection.DirectoryInfo, destinationName);

            throw new ArgumentException(string.Empty, nameof(item));
        }

        public static Task<bool> Move(this MailRuCloud cloud, IStoreItem item, string destinationName)
        {
            if (null == item) return Task.FromResult(false);

            var storeItem = item as MailruStoreItem;
            if (storeItem != null)
                return cloud.Move(storeItem.FileInfo, destinationName);
            var storeCollection = item as MailruStoreCollection;
            if (storeCollection != null)
                return cloud.Move(storeCollection.DirectoryInfo, destinationName);

            throw new ArgumentException(string.Empty, nameof(item));
        }

        public static string GetFullPath(this IStoreItem item)
        {
            if (null == item) return string.Empty;

            var storeItem = item as MailruStoreItem;
            if (storeItem != null)
                return storeItem.FullPath;
            var storeCollection = item as MailruStoreCollection;
            if (storeCollection != null)
                return storeCollection.FullPath;

            throw new ArgumentException(string.Empty, nameof(item));
        }


        public static long ContentLength(this IHttpRequest request)
        {
            long res;
            long.TryParse(request.GetHeaderValue("Content-Length"), out res);

            return res;
        }

        public static long BytesCount(this string value)
        {
            return Encoding.UTF8.GetByteCount(value);
        }
    }
}
