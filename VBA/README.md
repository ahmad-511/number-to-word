# Number to word converter using VBA script

## How to add NumberToWord.bas to your Excel workbook

- Open Visual Basic
    - Go to **Developer** tab 
    - Click **Visual Basic** icon
- From the **File** menu click **Import File ...**
- Find and select **NumberToWord_ar.bas** file and click **Open**

> If you couldn't see the **Developer** ribbon:\
> Go to **FILE** menu > **Options**\
> Under the **Customize the Ribbon** select **Main Tabs**\
> Make sure that **Developer** is checked then click **OK**

You can also copy the content of **NumberToWord_ar.bas** without the first line `Attribute VB_Name = "NumberToWord_ar"` and paste it in your own module file
## Usage:

In any cell in your Excel sheet type `=NumberToWordAR(NUMBER OR REFERENCE)`

It supports numbers from 0 to Nonillion (30 digits), Decimal places are truncated

> If the Arabic letters looked weird that's because Github doesn't support **ANSI** encoding required for VBA script with Arabic support.\
> To fix that, open the file in **notepad++** and from the **Encoding** menu select **Convert to ANSI** then save.
