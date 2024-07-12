function padLeft(str, char, paddingSize){
    if(str.length % paddingSize == 0){
        return str
    }
    
    return `${char.repeat(paddingSize - str.length % paddingSize)}${str}`
}

 function numberToWordAR(num){
    num = String(num).split('.')[0]
	const strNum = padLeft(num, '0', 3)
	const wholeWords = []

	const zeroToNineteen = ['صفر', 'واحد', 'إثنان', 'ثلاثة', 'أربعة', 'خمسة', 'ستة', 'سبعة', 'ثمانية', 'تسعة', 'عشرة', 'إحدى عشر', 'إثنا عشر', 'ثلاثة عشر', 'أربعة عشر', 'خمسة عشر', 'ستة عشر', 'سبعة عشر', 'ثمانية عشر', 'تسعة عشر']
	const multiplesOf10 = ['', 'عشرة', 'عشرون', 'ثلاثون', 'أربعون', 'خمسون', 'ستون', 'سبعون', 'ثمانون', 'تسعون']
	const multiplesOf100 = ['', 'مائة', 'مائتان']
	const multiplesOf1000Singular = ['', 'ألف', 'مليون', 'بليون', 'تريليون', 'كوادريليون', 'كوينتيليون', 'سيكستيليون', 'سيبتيليون', 'أوكتيليون', 'نونيليون']
	const multiplesOf1000Dual = ['', 'ألفان', 'مليونان', 'بليونان', 'تريليونان', 'كوادريليونان', 'كوينتيليونان', 'سيكستيليونان', 'سيبتيليونان', 'أوكتيليونان', 'نونيليون']
	const multiplesOf1000Plural = ['', 'ألاف', 'ملايين', 'بلايين', 'تريليونات', 'كوادريليونات', 'كوينتيليونات', 'سيكستيليونات', 'سيبتيليونات', 'أوكتيليونات', 'نونيليوت']

	const segmentsCount = strNum.length / 3

	for(let i = 0; i < segmentsCount; i++){
		const segment = strNum.substr(i * 3, 3)
		
		const segmentVal = parseInt(segment)
		const segmentIndex = segmentsCount - (i + 1)

		// Zero case
		if(num == 0){
			return zeroToNineteen[0]
        }

		if(segmentVal == 0){
			continue
        }

		let p1 = parseInt(segment.substr(0, 1))
		let p2 = parseInt(segment.substr(1, 1))
		let p3 = parseInt(segment.substr(2, 1))

		let word = []
		// Hundreds
		if(p1 > 0){
			if(p1 > 0 && p1 < 3){
				word.push(multiplesOf100[p1])
            }else{
				// Remove the ta'a when related to 100, it must be for example looks like ثلاث مائة
				word.push(zeroToNineteen[p1].replace('ة', '') + ' ' + multiplesOf100[1])
            }
        }

		// Ones
		if(p3 > 0){
			if(p2 == 1){
				p3 = p3 + 10
				// We don't need p2 anymore as it added to p3
				p2 = 0
            }

			word.push(zeroToNineteen[p3])
        }

		// Tens, start from 2 as p2=1 in included in p3 above, Exception is when p3=0
		if(p2 > 0 && p3 < 10){
			word.push(multiplesOf10[p2])
        }

		let segmentMultiplier = ''
		
		if(segmentsCount - i > multiplesOf1000Singular.length){
			word.push ('###')
        }else{
			if(segmentIndex > 0){
				if(segmentVal == 1){
					word = []
					word.push(multiplesOf1000Singular[segmentIndex])

                }else if(segmentVal == 2){
					word = []
					word.push(multiplesOf1000Dual[segmentIndex])

                }else if(segmentVal >= 3 && segmentVal <= 10){
					segmentMultiplier = ' ' + multiplesOf1000Plural[segmentIndex]
                }else{
					// segmentVal >= 11
					segmentMultiplier = ' ' + multiplesOf1000Singular[segmentIndex] + 'اً'
                }
            }
        }

		// Cleanup
		for(let j = 1; j < word.length; j++){
			if(word[j].trim() == ''){
				word.splice(j, 1)
				j = j - 1
            }
        }
		
		const wordStr = word.join(' و ')
		
		wholeWords.push(wordStr + segmentMultiplier)
    }

    if(wholeWords.length > 0){
	    return wholeWords.join(' و ')
    }
 }
