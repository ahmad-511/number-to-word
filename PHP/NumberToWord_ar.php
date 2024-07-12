<?php
function padLeft(string $str, string $char, int $paddingSize){
    if(strlen($str) % $paddingSize == 0){
        return $str;
    }
    
    return  str_repeat($char, $paddingSize - strlen($str) % $paddingSize) . $str;
}

 function numberToWordAR($num){
	$num = (string)$num;
    $num = explode('.', $num)[0];
	$strNum = padLeft($num, '0', 3);
	$wholeWords = [];

	$zeroToNineteen = ['صفر', 'واحد', 'إثنان', 'ثلاثة', 'أربعة', 'خمسة', 'ستة', 'سبعة', 'ثمانية', 'تسعة', 'عشرة', 'إحدى عشر', 'إثنا عشر', 'ثلاثة عشر', 'أربعة عشر', 'خمسة عشر', 'ستة عشر', 'سبعة عشر', 'ثمانية عشر', 'تسعة عشر'];
	$multiplesOf10 = ['', 'عشرة', 'عشرون', 'ثلاثون', 'أربعون', 'خمسون', 'ستون', 'سبعون', 'ثمانون', 'تسعون'];
	$multiplesOf100 = ['', 'مائة', 'مائتان'];
	$multiplesOf1000Singular = ['', 'ألف', 'مليون', 'بليون', 'تريليون', 'كوادريليون', 'كوينتيليون', 'سيكستيليون', 'سيبتيليون', 'أوكتيليون', 'نونيليون'];
	$multiplesOf1000Dual = ['', 'ألفان', 'مليونان', 'بليونان', 'تريليونان', 'كوادريليونان', 'كوينتيليونان', 'سيكستيليونان', 'سيبتيليونان', 'أوكتيليونان', 'نونيليون'];
	$multiplesOf1000Plural = ['', 'ألاف', 'ملايين', 'بلايين', 'تريليونات', 'كوادريليونات', 'كوينتيليونات', 'سيكستيليونات', 'سيبتيليونات', 'أوكتيليونات', 'نونيليوت'];

	$segmentsCount = strlen($strNum) / 3;

	for($i = 0; $i < $segmentsCount; $i++){
		$segment = substr($strNum, $i * 3, 3);
		
		$segmentVal = intval($segment);
		$segmentIndex = $segmentsCount - ($i + 1);

		// Zero case
		if($num == 0){
			return $zeroToNineteen[0];
        }

		if($segmentVal == 0){
			continue;
        }

		$p1 = intval(substr($segment, 0, 1));
		$p2 = intval(substr($segment, 1, 1));
		$p3 = intval(substr($segment, 2, 1));

		$word = [];
		// Hundreds
		if($p1 > 0){
			if($p1 > 0 && $p1 < 3){
				$word[] = $multiplesOf100[$p1];
            }else{
				// Remove the ta'a when related to 100, it must be for example looks like ثلاث مائة
				$word[] = str_replace('ة', '', $zeroToNineteen[$p1]) . ' ' . $multiplesOf100[1];
            }
        }

		// Ones
		if($p3 > 0){
			if($p2 == 1){
				$p3 = $p3 + 10;
				// We don't need p2 anymore as it added to p3
				$p2 = 0;
            }

			$word[] = $zeroToNineteen[$p3];
        }

		// Tens, start from 2 as p2=1 in included in p3 above, Exception is when p3=0
		if($p2 > 0 && $p3 < 10){
			$word[] = $multiplesOf10[$p2];
        }

		$segmentMultiplier = '';
		
		if($segmentsCount - $i > count($multiplesOf1000Singular)){
			$word[] = '###';
        }else{
			if($segmentIndex > 0){
				if($segmentVal == 1){
					$word = [];
					$word[] = $multiplesOf1000Singular[$segmentIndex];

                }elseif($segmentVal == 2){
					$word = [];
					$word[] = $multiplesOf1000Dual[$segmentIndex];

                }elseif($segmentVal >= 3 && $segmentVal <= 10){
					$segmentMultiplier = ' ' . $multiplesOf1000Plural[$segmentIndex];
                }else{
					// segmentVal >= 11
					$segmentMultiplier = ' ' . $multiplesOf1000Singular[$segmentIndex] . 'اً';
                }
            }
        }

		// Cleanup
		for($j = 1; $j < count($word); $j++){
			if(trim($word[$j]) == ''){
				array_splice($word, $j, 1);
				$j = $j - 1;
            }
        }

		$wordStr = implode(' و ', $word);
		$wholeWords[] = $wordStr . $segmentMultiplier;
    }

    if(count($wholeWords) > 0){
	    return implode(' و ', $wholeWords);
    }
 }

file_put_contents('./test.txt', numberToWordAR('9223372036854775807'));