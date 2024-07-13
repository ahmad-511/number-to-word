	Private Function PadLeft(str As String, chr As String, paddingSize As Integer) As String
		If str.Length Mod paddingSize = 0 Then
			Return str
		End If

		Return $"{StrDup(paddingSize - str.Length Mod paddingSize, chr)}{str}"
	End Function

	Public Function NumberToWordAR(num As BigInteger) As String
		Dim strNum As String = num.ToString().Split(".")(0)
		strNum = PadLeft(strNum, "0", 3)
		Dim wholeWords As New List(Of String)

		Dim zeroToNineteen As String() = {"صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "إحدى عشر", "إثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"}
		Dim multiplesOf10 As String() = {"", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون"}
		Dim multiplesOf100 As String() = {"", "مائة", "مائتان"}
		Dim multiplesOf1000Singular As String() = {"", "ألف", "مليون", "بليون", "تريليون", "كوادريليون", "كوينتيليون", "سيكستيليون", "سيبتيليون", "أوكتيليون", "نونيليون"}
		Dim multiplesOf1000Dual As String() = {"", "ألفان", "مليونان", "بليونان", "تريليونان", "كوادريليونان", "كوينتيليونان", "سيكستيليونان", "سيبتيليونان", "أوكتيليونان", "نونيليون"}
		Dim multiplesOf1000Plural As String() = {"", "ألاف", "ملايين", "بلايين", "تريليونات", "كوادريليونات", "كوينتيليونات", "سيكستيليونات", "سيبتيليونات", "أوكتيليونات", "نونيليوت"}

		Dim segmentsCount As Integer = strNum.Length / 3

		For i = 0 To segmentsCount - 1
			Dim segment = strNum.Substring(i * 3, 3)

			Dim segmentVal = CInt(segment)
			Dim segmentIndex = segmentsCount - (i + 1)

			' Zero case
			If num = 0 Then
				Return zeroToNineteen(0)
			End If

			If segmentVal = 0 Then
				Continue For
			End If

			Dim p1 = CInt(segment.Substring(0, 1))
			Dim p2 = CInt(segment.Substring(1, 1))
			Dim p3 = CInt(segment.Substring(2, 1))

			Dim word As New List(Of String)
			' Hundreds
			If p1 > 0 Then
				If p1 > 0 And p1 < 3 Then
					word.Add(multiplesOf100(p1))
				Else
					' Remove the ta'a when related to 100, it must be for example looks like ثلاث مائة
					word.Add(zeroToNineteen(p1).Replace("ة", "") & " " & multiplesOf100(1))
				End If
			End If

			' Ones
			If p3 > 0 Then
				If p2 = 1 Then
					p3 = p3 + 10
					' We don't need p2 anymore as it added to p3
					p2 = 0
				End If

				word.Add(zeroToNineteen(p3))
			End If

			' Tens, start from 2 as p2=1 in included in p3 above, Exception Is when p3=0
			If p2 > 0 And p3 < 10 Then
				word.Add(multiplesOf10(p2))
			End If

			Dim segmentMultiplier = ""

			If segmentsCount - i > multiplesOf1000Singular.Length Then
				word.Add("###")
			Else
				If segmentIndex > 0 Then
					If segmentVal = 1 Then
						word.Clear()
						word.Add(multiplesOf1000Singular(segmentIndex))

					ElseIf segmentVal = 2 Then
						word.Clear()
						word.Add(multiplesOf1000Dual(segmentIndex))

					ElseIf segmentVal >= 3 And segmentVal <= 10 Then
						segmentMultiplier = " " & multiplesOf1000Plural(segmentIndex)
					Else
						' segmentVal >= 11
						segmentMultiplier = " " & multiplesOf1000Singular(segmentIndex) & "اً"
					End If
				End If
			End If

			' Cleanup
			For j = 1 To word.Count - 1
				If word(j).Trim() = "" Then
					word.RemoveAt(j)
					j = j - 1
				End If
			Next

			Dim wordStr As String = Join(word.ToArray(), " و ")

			wholeWords.Add(wordStr & segmentMultiplier)
		Next

		If wholeWords.Count > 0 Then
			Return Join(wholeWords.ToArray, " و ")
		End If

		Return ""
	End Function