using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyPower.Factory
{
    /// <summary>
    /// 实体框架的数据访问层接口的构造工厂。
    /// </summary>
    public class DALFactory
    {
        private static readonly string AssemblyPath = "MyPower";
        //普通局部变量
        private static Hashtable objCache = new Hashtable();
        private static object syncRoot = new Object();
        private static DALFactory m_Instance = null;

        /// <summary>
        /// IOC的容器，可调用来获取对应接口实例。
        /// </summary>
        public IUnityContainer Container { get; set; }

        /// <summary>
        /// 创建或者从缓存中获取对应业务类的实例
        /// </summary>
        public static DALFactory Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new DALFactory();
                            //初始化相关的注册接口
                            m_Instance.Container = new UnityContainer();

                            //根据约定规则自动注册DAL
                            RegisterDAL(m_Instance.Container);
                        }
                    }
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// 使用Unity自动加载对应的IDAL接口的实现（DAL层）
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterDAL(IUnityContainer container)
        {
            Dictionary<string, Type> dictInterface = new Dictionary<string, Type>();
            Dictionary<string, Type> dictDAL = new Dictionary<string, Type>();
            Assembly currentAssemblyDAL = Assembly.Load(string.Format("{0}.DAL", AssemblyPath));//加载 DAL 程序集
            Assembly currentAssemblyIDAL = Assembly.Load(string.Format("{0}.IDAL", AssemblyPath));//加载 IDAL 程序集
            string dalSuffix = ".DAL";
            string interfaceSuffix = ".IDAL";

            string defaultNamespace = string.Empty;
            //对比程序集里面的接口和具体的接口实现类，把它们分别放到不同的字典集合里
            foreach (Type objType in currentAssemblyIDAL.GetTypes())
            {
                defaultNamespace = objType.Namespace;
                if (objType.IsInterface && defaultNamespace.EndsWith(interfaceSuffix))
                {
                    if (!dictInterface.ContainsKey(objType.FullName))
                    {
                        dictInterface.Add(objType.FullName, objType);
                    }
                }
            }
            //对比程序集里面的接口和具体的接口实现类，把它们分别放到不同的字典集合里
            foreach (Type objType in currentAssemblyDAL.GetTypes())
            {
                defaultNamespace = objType.Namespace;
                if (defaultNamespace.EndsWith(dalSuffix))
                {
                    if (!dictDAL.ContainsKey(objType.FullName))
                    {
                        dictDAL.Add(objType.FullName, objType);
                    }
                }
            }

            //根据注册的接口和接口实现集合，使用IOC容器进行注册
            foreach (string key in dictInterface.Keys)
            {
                Type interfaceType = dictInterface[key];
                foreach (string dalKey in dictDAL.Keys)
                {
                    Type dalType = dictDAL[dalKey];
                    if (interfaceType.IsAssignableFrom(dalType))//判断DAL是否实现了某接口
                    {
                        container.RegisterType(interfaceType, dalType);
                    }
                }
            }
        }
    }
}
