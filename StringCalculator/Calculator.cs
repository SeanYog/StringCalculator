using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator
{
	public class Calculator
	{
		public int Add(string numbers)
		{
			var separators = new List<string> { ",", "\n" };

			if (numbers.StartsWith("//"))
			{
				var firstLineSplit = numbers.Split(new char[] { '\n' }, 2);
				var customSeperatorsString = firstLineSplit[0]
					.Replace("//", string.Empty)
					.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

				//customSeperatorsString = customSeperatorsString
				//	.Select(x => x.Trim('['))
				//	.Select(x => x.Trim(']'))
				//	.ToArray();

				separators.AddRange(customSeperatorsString);
				numbers = firstLineSplit.Last();
			}

			var seperatorsArray = separators.ToArray();

			var splitNumbers = numbers
				.Split(seperatorsArray, StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse)
				.ToList();

			splitNumbers = splitNumbers.Where(x => x <= 1000).ToList();

			var negatives = splitNumbers.Where(x => x < 0).ToList();
			if (negatives.Any())
			{
				throw new Exception("Negatives not allowed: " + 
					string.Join(',', negatives));
			}

			return splitNumbers.Sum();
		}
	}
}
