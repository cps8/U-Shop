using Microsoft.Extensions.Configuration;

namespace UShop.Common
{
    public class ConfigUtils
    {
        private static IConfiguration? _configuration;

        // 单例模式：确保配置全局唯一
        private static readonly Lazy<ConfigUtils> _instance = new(() => new ConfigUtils());

        public static ConfigUtils Instance => _instance.Value;

        // 私有构造函数，防止直接实例化
        private ConfigUtils() { }

        /// <summary>
        /// 初始化配置（用于控制台应用）
        /// </summary>
        /// <param name="configuration">传入的 IConfiguration 对象</param>
        public static void Initialize(IConfiguration configuration)
        {
            if (_configuration != null)
            {
                throw new InvalidOperationException("ConfigHelper has already been initialized.");
            }
            _configuration = configuration;
        }

        /// <summary>
        /// 默认初始化方法（控制台应用的便捷方法）
        /// </summary>
        public static void InitializeDefault()
        {
            if (_configuration != null) return;

            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">Key, 如果对象层级较深需要用`:`分隔</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public string Get(string key)
        {
            EnsureInitialized();
            return _configuration?[key] ?? throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
        }

        /// <summary>
        /// 获取指定类型的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string value = Get(key);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// 获取配置节并绑定到对象
        /// </summary>
        public T GetSection<T>(string sectionName) where T : new()
        {
            EnsureInitialized();
            var section = new T();
            _configuration?.GetSection(sectionName).Bind(section);
            return section;
        }

        /// <summary>
        /// 确保配置已初始化
        /// </summary>
        private void EnsureInitialized()
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("ConfigHelper is not initialized. Call Initialize() first.");
            }
        }
    }
}
