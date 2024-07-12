# Number to word converter using JavaScript

## How to add NumberToWord.js to your Excel workbook

Include **NumberToWord_ar.js** file in your html file
```html
<script src="./NumberToWord_ar.js"></script>
```

Or copy both functions **padLeft** and **numberToWordAR** and paste them inside your a `script` tag
```js
function padLeft(){...}

function numberToWordAR(){...}
```

## Usage:

```js
console.log(numberToWordAR(12345))
console.log(numberToWordAR('987654'))
console.log(numberToWordAR(12345678901234567890n)) // Big Integer
console.log(numberToWordAR('12345678901234567890')) // Big Integer
```

It supports numbers from 0 to Nonillion (30 digits), Decimal places are truncated

> Big integers can be passed as **String** or by appending **n** to the end of it
