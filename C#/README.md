# Number to word converter using C#

## How to add NumberToWord.php to your project

Copy both functions **PadLeft** and **NumberToWordAR** and paste them into your C# class or working file
```cs
string PadLeft(){...}

string NumberToWordAR(){...}
```

## Usage:

```cs
string a = NumberToWordAR(12345)
string b = NumberToWordAR(System.Numerics.BigInteger.Parse("1844674407370777955161777776665777")) // Big integer
```

It supports numbers from 0 to Nonillion (30 digits), Decimal places are truncated

> When dealing with big integers (greater than 10^19 you need to use the BigInteger.Parse function from the System.Numeric namespace)