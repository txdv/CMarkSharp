using System;

namespace CMarkSharp
{
	class Ensure
	{
		public static void Success(int r)
		{
			if (r != 0) {
				throw new Exception();
			}
		}

		public static void ArgumentNotNull(object obj, string paramName)
		{
			if (obj == null) {
				throw new ArgumentNullException(paramName);
			}
		}

		public static void ArgumentNotNegative(int number, string paramName)
		{
			if (number < 0) {
				throw new ArgumentOutOfRangeException(paramName);
			}
		}

		public static void InRange(byte[] bytes, string bytesName, int index, string indexName, int count, string countName)
		{
			Ensure.ArgumentNotNull(bytes, bytesName);
			Ensure.ArgumentNotNegative(bytes.Length - index, indexName);
			Ensure.ArgumentNotNegative(bytes.Length - index - count, countName);
		}

		public static void ArgumentNotDefault<T>(T strukt, string struktName) where T : struct
		{
			if (strukt.Equals(default(T))) {
				throw new ArgumentException("{0} shouldn't be default", struktName);
			}
		}
	}
}

