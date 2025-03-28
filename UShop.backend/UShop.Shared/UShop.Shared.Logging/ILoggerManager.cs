using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UShop.Shared.Logging
{
    public interface ILoggerManager
    {
        /// <summary>
        /// 记录info级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Info(string message);

        /// <summary>
        /// 记录warn级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Warn(string message);

        /// <summary>
        /// 记录debug级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Debug(string message);

        /// <summary>
        /// 记录error级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Error(string message);

        /// <summary>
        /// 记录error级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常</param>
        void Error(string message, Exception ex);

        /// <summary>
        /// 记录debug级别日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">日志内容</param>
        /// <param name="data">额外数据</param>
        void Debug<T>(string message, T data);

        /// <summary>
        /// 记录info级别日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">日志内容</param>
        /// <param name="data">额外数据</param>
        void Info<T>(string message, T data);

        /// <summary>
        /// 记录warn级别日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">日志内容</param>
        /// <param name="data">额外数据</param>
        void Warn<T>(string message, T data);

        /// <summary>
        /// 记录error级别日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">日志内容</param>
        /// <param name="data">额外数据</param>
        void Error<T>(string message, T data);

        /// <summary>
        /// 记录批量操作日志 - info级别
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="items"></param>
        void BatchInfo<T>(string message, params T[] items);
    }
}
