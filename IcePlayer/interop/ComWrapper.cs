using System;
using System.Runtime.InteropServices;

namespace IcePlayer.Interop
{
    /// <summary>
    /// <see cref="System.IDisposable"/> を実装し COM オブジェクトを自動的に解放するラッパーを提供します。
    /// </summary>
    /// <typeparam name="T">COM オブジェクトの型。</typeparam>
    public class ComWrapper<T> : ComWrapper, IDisposable
    {
        /// <summary>
        /// COM オブジェクトを取得します。
        /// </summary>
        public T Object { get; }

        internal ComWrapper(T obj)
        {
            // Object の取得を行います。
            this.Object = obj;
        }

        /// <summary>
        /// ガベージ コレクターがオブジェクトを破棄する前に、最後のクリーンアップを実行します。
        /// </summary>
        ~ComWrapper()
        {
            // クリーンアップの実行 (Dispose)
            Dispose( false );
        }

        /// <summary>
        /// COM を解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// COM の解放。
        /// </summary>
        /// <param name="disposing">明示的な破棄要求元から呼び出された場合は true に設定します。それ以外の場合は false。</param>
        protected virtual void Dispose( bool disposing )
        {
            if ( disposing && this.Object != null && Marshal.IsComObject( this.Object ) )
                Marshal.FinalReleaseComObject( this.Object );
            return;
        }
    }

    /// <summary>
    /// COM オブジェクトを自動的に解放するラッパーを表します。
    /// </summary>
    public class ComWrapper
    {
        internal ComWrapper() { }

        /// <summary>
        /// 指定した COM オブジェクトのラッパーとして機能する <see cref="IcePlayer.Interop.ComWrapper"/> を作成します。
        /// </summary>
        /// <typeparam name="T">COM オブジェクトの型。</typeparam>
        /// <param name="obj">COM オブジェクト。</param>
        /// <returns>COM オブジェクトのラッパー。</returns>
        public static ComWrapper<T> Create<T>( T obj ) => new ComWrapper<T>( obj );
    }
}
