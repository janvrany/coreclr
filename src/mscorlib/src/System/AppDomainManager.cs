// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


//
// An AppDomainManager gives a hosting application the chance to 
// participate in the creation and control the settings of new AppDomains.
//

namespace System
{
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Runtime.InteropServices;

    [System.Security.SecurityCritical]  // auto-generated_required
    [System.Runtime.InteropServices.ComVisible(true)]
    public class AppDomainManager : MarshalByRefObject
    {
        public AppDomainManager () {}

        [System.Security.SecurityCritical]
        public virtual void InitializeNewDomain (AppDomainSetup appDomainInfo)
        {
            // By default, InitializeNewDomain does nothing. AppDomain.CreateAppDomainManager relies on this fact.
        }

        [DllImport(JitHelpers.QCall, CharSet = CharSet.Unicode), SuppressUnmanagedCodeSecurity]
        private static extern void GetEntryAssembly(ObjectHandleOnStack retAssembly);

        private Assembly m_entryAssembly = null;
        public virtual Assembly EntryAssembly {
            [System.Security.SecurityCritical]  // auto-generated
            get {
                // The default AppDomainManager sets the EntryAssembly depending on whether the
                // AppDomain is a manifest application domain or not. In the first case, we parse
                // the application manifest to find out the entry point assembly and return that assembly.
                // In the second case, we maintain the old behavior by calling GetEntryAssembly().
                if (m_entryAssembly == null)
                {
                    {
                        RuntimeAssembly entryAssembly = null;
                        GetEntryAssembly(JitHelpers.GetObjectHandleOnStack(ref entryAssembly));
                        m_entryAssembly = entryAssembly;
                    }
                }
                return m_entryAssembly;
            }
        }

        internal static AppDomainManager CurrentAppDomainManager {
            [System.Security.SecurityCritical]  // auto-generated
            get {
                return AppDomain.CurrentDomain.DomainManager;
            }
        }

        public virtual bool CheckSecuritySettings (SecurityState state)
        {
            return false;
        }
    }
}
