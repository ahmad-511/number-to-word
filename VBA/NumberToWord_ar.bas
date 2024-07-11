Attribute VB_Name = "NumberToWord_ar"
Function CollectionToArray(collec As Collection) As Variant
	Dim arr() As String
	ReDim arr(collec.Count - 1)
	
	Dim i As Integer
		For i = 0 To collec.Count - 1
		arr(i) = collec.Item(i + 1)
	Next
	
	CollectionToArray = arr
End Function

Function PadLeft(str As String, char As String, paddingSize As Integer) As String
	If Len(str) Mod paddingSize = 0 Then
        PadLeft = str
    Else
        PadLeft = String(paddingSize - Len(str) Mod paddingSize, char) & str
    End If
End Function

 Function NumberToWordAR(num As Double) As String
    num = Fix(num)

	Dim strNum As String
	strNum = num
	strNum = PadLeft(strNum, "0", 3)

	Dim wholeWords As New Collection

	Dim zeroToNineteen As Variant
	zeroToNineteen = Array("صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "إحدى عشر", "إثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر")
	
	Dim multiplesOf10 As Variant
	multiplesOf10 = Array("", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون")
	
	Dim multiplesOf100 As Variant
	multiplesOf100 = Array("", "مائة", "مائتان")
	
	Dim multiplesOf1000Singular As Variant
	multiplesOf1000Singular = Array("", "ألف", "مليون", "بليون", "تريليون", "كوادريليون", "كوينتيليون", "سيكستيليون", "سيبتيليون", "أوكتيليون", "نونيليون")
	
	Dim multiplesOf1000Dual As Variant
	multiplesOf1000Dual = Array("", "ألفان", "مليونان", "بليونان", "تريليونان", "كوادريليونان", "كوينتيليونان", "سيكستيليونان", "سيبتيليونان", "أوكتيليونان", "نونيليون")
	
	Dim multiplesOf1000Plural As Variant
	multiplesOf1000Plural = Array("", "ألاف", "ملايين", "بلايين", "تريليونات", "كوادريليونات", "كوينتيليونات", "سيكستيليونات", "سيبتيليونات", "أوكتيليونات", "نونيليوت")

	Dim segmentsCount As Integer
	segmentsCount = Len(strNum) / 3

	Dim i As Integer
	For i = 0 To segmentsCount
		Dim segment As String
		segment = Mid(strNum, i * 3 + 1, 3)
		
		Dim segmentVal As Integer
		segmentVal = Val(segment)
		
		Dim segmentIndex As Integer
		segmentIndex = segmentsCount - (i + 1)

		' Zero case
		If num = 0 Then
			NumberToWordAR = zeroToNineteen(0)
		End If

		If segmentVal = 0 Then
			GoTo NextLoop
		End If

		Dim p1 As Integer
		p1 = Val(Left(segment, 1))
		
		Dim p2 As Integer
		p2 = Val(Mid(segment, 2, 1))
		
		Dim p3 As Integer
		p3 = Val(Right(segment, 1))

		Dim word As New Collection
		Set word = Nothing
		' Hundreds
		If p1 > 0 Then
			If p1 > 0 And p1 < 3 Then
				word.Add multiplesOf100(p1)
			Else
				' Remove the ta'a when related to 100, it must be for example looks like ثلاث مائة
				word.Add Replace(zeroToNineteen(p1), "ة", "") & " " & multiplesOf100(1)
			End If
		End If

		' Ones
		If p3 > 0 Then
			If p2 = 1 Then
				p3 = p3 + 10
				' We don't need p2 anymore as it added to p3
				p2 = 0
			End If

			word.Add zeroToNineteen(p3)
		End If

		' Tens, start from 2 as p2=1 in included in p3 above, Exception is when p3=0
		If p2 > 0 And p3 < 10 Then
			word.Add multiplesOf10(p2)
		End If

		Dim segmentMultiplier As String
		segmentMultiplier = ""
		
		If segmentsCount - i > UBound(multiplesOf1000Singular) Then
			word.Add ("###")
		Else
			If segmentIndex > 0 Then
				If segmentVal = 1 Then
					Set word = Nothing
					word.Add (multiplesOf1000Singular(segmentIndex))

				ElseIf segmentVal = 2 Then
					Set word = Nothing
					word.Add (multiplesOf1000Dual(segmentIndex))

				ElseIf segmentVal >= 3 And segmentVal <= 10 Then
					segmentMultiplier = " " & multiplesOf1000Plural(segmentIndex)
				Else
					' segmentVal >= 11
					segmentMultiplier = " " & multiplesOf1000Singular(segmentIndex) & "اً"
				End If
			End If
		End If

		' Cleanup
		Dim j As Integer
		
		For j = 1 To word.Count
			If Trim(word.Item(j)) = "" Then
				word.Remove (j)
				j = j - 1
			End If
		Next
		
		Dim wordStr As String
		wordStr = Join(CollectionToArray(word), " و ")
		
		wholeWords.Add wordStr & segmentMultiplier
		
NextLoop:
	Next

    If wholeWords.count > 0 Then
	    NumberToWordAR = Join(CollectionToArray(wholeWords), " و ")
    End If
End Function

