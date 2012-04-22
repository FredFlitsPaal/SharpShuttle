using Shared.Communication.Exceptions;

namespace Shared.Communication
{
	/// <summary>
	/// Een class om de andwoorden van requests te casten naar hun echte type
	/// </summary>
	public static class ResponseCaster
	{
		/// <summary>
		/// Cast het antwoord van een request naar het type 
		/// </summary>
		/// <typeparam name="T">Het type om naar te casten</typeparam>
		/// <param name="Response">De RequestContainer die het antwoord bevat</param>
		/// <exception cref="WrongTypedAnswerException">Het antwoord is niet van type </exception>
		/// <exception cref="CommunicationException">Als de response een exception bevat</exception>
		public static T CastAnswer<T>(RequestContainer Response) where T : class
		{
			object answer = Response.Answer;

			//Als het antwoord een ResponseMessage is dan kijken of hij een Exception heeft
			ResponseMessage resp = answer as ResponseMessage;
			if (resp != null)
			{
				CommunicationException exception = resp.Exception;
				if (exception != null)
					throw exception;
			}

			T castresult = answer as T;

			if (castresult != null)
				return castresult;

			throw new WrongTypedAnswerException();
		}
	}
}
