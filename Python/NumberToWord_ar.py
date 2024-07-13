def PadLeft(str, char, paddingSize):
    if len(str) % paddingSize == 0:
        return str
    
    return f"{char * (paddingSize - len(str) % paddingSize)}{str}"

def NumberToWordAR(num):
    strNum = str(num).split(".")[0]
    strNum = PadLeft(strNum, "0", 3)
    wholeWords = []

    zeroToNineteen = [ "صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "إحدى عشر", "إثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"]
    multiplesOf10 = ["", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون"]
    multiplesOf100 = ["", "مائة", "مائتان"]
    multiplesOf1000Singular = ["", "ألف", "مليون", "بليون", "تريليون", "كوادريليون", "كوينتيليون", "سيكستيليون", "سيبتيليون", "أوكتيليون", "نونيليون"]
    multiplesOf1000Dual = ["", "ألفان", "مليونان", "بليونان", "تريليونان", "كوادريليونان", "كوينتيليونان", "سيكستيليونان", "سيبتيليونان", "أوكتيليونان", "نونيليون"]
    multiplesOf1000Plural = ["", "ألاف", "ملايين", "بلايين", "تريليونات", "كوادريليونات", "كوينتيليونات", "سيكستيليونات", "سيبتيليونات", "أوكتيليونات", "نونيليوت"]

    segmentsCount = len(strNum) / 3

    for i in range(int(segmentsCount)):
        segment = strNum[i * 3: i * 3 + 3]
        segmentVal = int(segment)
        segmentIndex = int(segmentsCount - (i + 1))

		# Zero case
        if num == 0:
            return zeroToNineteen[0]

        if segmentVal == 0:
            continue

        p1 = int(segment[0: 1])
        p2 = int(segment[1: 2])
        p3 = int(segment[2: 3])

        word = []

        # Hundreds
        if p1 > 0:
            if p1 > 0 and p1 < 3:
                word.append(multiplesOf100[p1])
            else:
				# Remove the ta'a when related to 100, it must be for example looks like ثلاث مائة
                word.append(zeroToNineteen[p1].replace("ة", "") + " " + multiplesOf100[1])

        # Ones
        if p3 > 0:
            if p2 == 1:
                p3 = p3 + 10

				# We don't need p2 anymore as it added to p3
                p2 = 0

            word.append(zeroToNineteen[p3])

		# Tens, start from 2 as p2=1 in included in p3 above, Exception Is when p3=0
        if p2 > 0 and p3 < 10:
            word.append(multiplesOf10[p2])

        segmentMultiplier = ""

        if segmentsCount - i > len(multiplesOf1000Singular):
            word.append("###")
        else:
            if segmentIndex > 0:
                if segmentVal == 1:
                    word.clear()
                    word.append(multiplesOf1000Singular[segmentIndex])
                elif segmentVal == 2:
                    word.clear()
                    word.append(multiplesOf1000Dual[segmentIndex])
                elif segmentVal >= 3 and segmentVal <= 10:
                    segmentMultiplier = " " + multiplesOf1000Plural[segmentIndex]
                else:
					# segmentVal >= 11
                    segmentMultiplier = " " + multiplesOf1000Singular[segmentIndex] + "اً"
        
        # Cleanup
        for j in range(1, len(word)):
            if word[j].strip() == "":
                del word[j]
                j = j - 1
		
        wordStr = " و ".join(word)
        wholeWords.append(wordStr + segmentMultiplier)
    
    if len(wholeWords) > 0:
        return " و ".join(wholeWords)

    return ""
