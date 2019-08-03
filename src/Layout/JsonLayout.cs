using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using Newtonsoft.Json;

namespace log4net.Layout
{
    /// <summary>
    /// Json格式化
    /// </summary>
    public class JsonLayout : LayoutSkeleton
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonLayout()
        {
            base.IgnoresException = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ActivateOptions()
        {
            
        }

        /// <summary>
        /// 格式化方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="loggingEvent"></param>
        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (loggingEvent == null)
            {
                throw new ArgumentNullException("loggingEvent");
            }

            ExceptionEntity exceptionobj = null;
            if (loggingEvent.ExceptionObject != null)
            {
                exceptionobj = new ExceptionEntity(loggingEvent.ExceptionObject.Message, loggingEvent.ExceptionObject.StackTrace);
            }
                
            var dic = new Dictionary<string, object>
            {
                ["level"] = loggingEvent.Level.DisplayName,
                ["operationTime"] = loggingEvent.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss"),

                ["data"] = loggingEvent.MessageObject,
                ["exception"] = exceptionobj,            
            };
            
            writer.Write(JsonConvert.SerializeObject(dic));
            writer.WriteLine();
        }

        /// <summary>
        /// 异常实体类
        /// </summary>
        private class ExceptionEntity
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="eMessage"></param>
            /// <param name="eStackTrace"></param>
            public ExceptionEntity(string eMessage, string eStackTrace)
            {
                this.eMessage = eMessage;
                this.eStackTrace = eStackTrace;
            }

            private string m_message;
            /// <summary>
            /// 
            /// </summary>
            public string eMessage
            {
                get { return this.m_message; }
                set { this.m_message = value; }
            }

            private string m_stacktrace;
            /// <summary>
            /// 
            /// </summary>
            public string eStackTrace
            {
                get { return this.m_stacktrace; }
                set { this.m_stacktrace = value; }
            }
        }
    }
}
