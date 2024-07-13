string PadLeft(string str, char chr, int paddingSize)
{
    if(str.Length % paddingSize == 0){
		return str;
	}

	return $"{new string(chr, paddingSize - str.Length % paddingSize)}{str}";
}

string NumberToWordAR(BigInteger num)
{
	string strNum = num.ToString().Split(".")[0];
	strNum = PadLeft(strNum, '0', 3);
    List<string> wholeWords = new();

    string[] zeroToNineteen = [ "صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "إحدى عشر", "إثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"];
	string[] multiplesOf10 = ["", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون"];
	string[] multiplesOf100 = ["", "مائة", "مائتان"];
	string[] multiplesOf1000Singular = ["", "ألف", "مليون", "بليون", "تريليون", "كوادريليون", "كوينتيليون", "سيكستيليون", "سيبتيليون", "أوكتيليون", "نونيليون"];
	string[] multiplesOf1000Dual = ["", "ألفان", "مليونان", "بليونان", "تريليونان", "كوادريليونان", "كوينتيليونان", "سيكستيليونان", "سيبتيليونان", "أوكتيليونان", "نونيليون"];
	string[] multiplesOf1000Plural = ["", "ألاف", "ملايين", "بلايين", "تريليونات", "كوادريليونات", "كوينتيليونات", "سيكستيليونات", "سيبتيليونات", "أوكتيليونات", "نونيليوت"];

	int segmentsCount = strNum.Length / 3;

	for(int i = 0; i < segmentsCount; i++)
	{
		string segment = strNum.Substring(i * 3, 3);
		int segmentVal = int.Parse(segment);
		int segmentIndex = segmentsCount - (i + 1);

		// Zero case
		if (num == 0)
		{
			return zeroToNineteen[0];
		}

        if(segmentVal == 0)
		{
			continue;
		}

		int p1 = int.Parse(segment.Substring(0, 1));
		int p2 = int.Parse(segment.Substring(1, 1));
		int p3 = int.Parse(segment.Substring(2, 1));

		List<string> word = new();

		// Hundreds
		if (p1 > 0)
		{
			if (p1 > 0 && p1 < 3)
			{
				word.Add(multiplesOf100[p1]);
			}
			else
			{
				// Remove the ta'a when related to 100, it must be for example looks like ثلاث مائة
				word.Add(zeroToNineteen[p1].Replace("ة", "") + " " + multiplesOf100[1]);
			}
		}

		// Ones
		if (p3 > 0)
		{
			if (p2 == 1)
			{
				p3 = p3 + 10;

				// We don't need p2 anymore as it added to p3
				p2 = 0;
			}

			word.Add(zeroToNineteen[p3]);
		}

		// Tens, start from 2 as p2=1 in included in p3 above, Exception Is when p3=0
		if (p2 > 0 && p3 < 10) {
			word.Add(multiplesOf10[p2]);
		}

		string segmentMultiplier = "";

		if (segmentsCount - i > multiplesOf1000Singular.Length) {
			word.Add("###");
		}
		else {
			if (segmentIndex > 0) {
				if (segmentVal == 1) {
					word.Clear();
					word.Add(multiplesOf1000Singular[segmentIndex]);
				}
				else if(segmentVal == 2){
					word.Clear();
					word.Add(multiplesOf1000Dual[segmentIndex]);
				}else if(segmentVal >= 3 && segmentVal <= 10){
					segmentMultiplier = " " + multiplesOf1000Plural[segmentIndex];
				}
				else {
					// segmentVal >= 11
					segmentMultiplier = " " + multiplesOf1000Singular[segmentIndex] + "اً";
				}
			}
		}

		// Cleanup

		for (int j = 1; j < word.Count; j++) {
			if (word[j].Trim() == "") {
				word.RemoveAt(j);
				j = j - 1;
			}
		}
		string wordStr = String.Join(" و ", word.ToArray());
		wholeWords.Add(wordStr + segmentMultiplier);
    }

	if (wholeWords.Count > 0) {
		return String.Join(" و ", wholeWords.ToArray());
	}

	return "";
}