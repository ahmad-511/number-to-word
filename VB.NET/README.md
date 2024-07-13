# Number to word converter using Visual Basic dot Net

## How to add NumberToWord.vb to your project

Copy both functions **PadLeft** and **NumberToWordAR** and paste them into your vb.net class or module file
```vb
Private Function PadLeft(){...}

Public Function NumberToWordAR(){...}
```

## Usage:

```vb
Dim a as string = NumberToWordAR(12345)
Dim b as string = NumberToWordAR(System.Numerics.BigInteger.Parse("1844674407370777955161777776665777")) ' Big integer
```

It supports numbers from 0 to Nonillion (30 digits), Decimal places are truncated

> When dealing with big integers (greater than 10^19 you need to use the BigInteger.Parse function from the System.Numeric namespace)