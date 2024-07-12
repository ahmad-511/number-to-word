# Number to word converter using PHP

## How to add NumberToWord.php to your Excel workbook

Require **NumberToWord_ar.php** file
```php
require_once 'NumberToWord_ar.php';
```

Or copy both functions **padLeft** and **numberToWordAR** and paste them into your php file
```php
function padLeft(){...}

function numberToWordAR(){...}
```

## Usage:

```php
echo numberToWordAR(12345);
echo numberToWordAR('987654');
echo numberToWordAR('12345678901234567890'); // Big Integer
```

It supports numbers from 0 to Nonillion (30 digits), Decimal places are truncated

> Big integers (*greater than 9223372036854775807 in 64 bit systems*) can be passed as **String**
