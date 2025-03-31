using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UShop.Shared.Common
{
    /// <summary>
    /// 扩展类 - 链式加密
    /// </summary>
    public static class ObjectCryptExtensions
    {
        private const string _aesKey = "ZKlrnczW3K9qmxuVbKC0dzdmrefxeHlW";
        public static string ToAesEncrypt(this object? value)
        {
            if (value == null)
            {
                return "";
            }
            string text = JsonSerializer.Serialize(value);
            return CryptoUtils.AesEncrypt(text, _aesKey);
        }
        public static string ToAesEncrypt(this long? value)
        {
            return ((object?)value).ToAesEncrypt();
        }
        public static string ToAesEncrypt(this long value)
        {
            return ((object)value).ToAesEncrypt();
        }

        public static T? ToAesDecrypt<T>(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }
            string text = CryptoUtils.AesDecrypt(value, _aesKey);
            return JsonSerializer.Deserialize<T>(text);
        }
    }
}
