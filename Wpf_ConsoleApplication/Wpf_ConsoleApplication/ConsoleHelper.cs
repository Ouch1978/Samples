﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;


namespace Wpf_ConsoleApplication
{
    [SuppressUnmanagedCodeSecurity]
    public static class ConsoleHelper
    {
        private const string Kernel32_DllName = "kernel32.dll";

        [DllImport( Kernel32_DllName )]
        private static extern bool AllocConsole();

        [DllImport( Kernel32_DllName )]
        private static extern bool FreeConsole();

        [DllImport( Kernel32_DllName )]
        private static extern IntPtr GetConsoleWindow();

        [DllImport( Kernel32_DllName )]
        private static extern int GetConsoleOutputCP();

        public static bool HasConsole
        {
            get
            {
                return GetConsoleWindow() != IntPtr.Zero;
            }
        }

        public static void Show()
        {
            if( !HasConsole )
            {
                AllocConsole();
                InvalidateOutAndError();
            }
        }

        public static void Hide()
        {
            if( HasConsole )
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
        }

        public static void Toggle()
        {
            if( HasConsole )
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        static void InvalidateOutAndError()
        {
            Type type = typeof( System.Console );
            System.Reflection.FieldInfo _out = type.GetField( "_out" , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
            System.Reflection.FieldInfo _error = type.GetField( "_error" , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
            System.Reflection.MethodInfo _InitializeStdOutError = type.GetMethod( "InitializeStdOutError" , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
            _out.SetValue( null , null ); _error.SetValue( null , null );
            _InitializeStdOutError.Invoke( null , new object[] { true } );
        }

        static void SetOutAndErrorNull()
        {
            Console.SetOut( TextWriter.Null );
            Console.SetError( TextWriter.Null );
        }

    }
}