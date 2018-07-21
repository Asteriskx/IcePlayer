using System;
using System.Runtime.Serialization;

namespace IcePlayer.Exceptions
{
	/// <summary>
	/// IcePlayer で起きた例外を管理します。
	/// </summary>
	[Serializable()]
	public class IcePlayerException : Exception
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public IcePlayerException() : base("") { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		public IcePlayerException(string msg) : base(msg) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="inner"></param>
		public IcePlayerException(string msg, Exception inner) : base(msg, inner) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected IcePlayerException(SerializationInfo info, StreamingContext context) { }

		#endregion Constructors
	}
}
