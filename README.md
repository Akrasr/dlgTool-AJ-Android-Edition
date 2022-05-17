# dlgTool-AJ-Android-Edition
dlgTool modification for AJ mobile port script

This is the modification of onepiecefreak's dlgTool: https://github.com/onepiecefreak3/dlgTool

# Changes
1. The condPointers placing bug is fixed.
2. The sections lengths bugs are fixed.
3. The commands and their arguments lists now have the right values.
4. AJ script glyphs code bug is fixed.
5. The support for AJ3DS and AATrilogy3DS is deleted.
6. The support for AJ mobile port script is added.
7. The font and game params are no longer supported.

I have also added Russian glyphs since I am working on Russian translation of AJ.
You may find some leftover code for AATrilogy and AJ3DS support.

# AJ Mobile Support
1. Glyphs codes from mobile port in Tables.cs.
2. The script is not compressed, but encoded by TripleDes with some key. The key was found in game files.

# Where is AJ mobile script?
You can find it in this game's cache obb file. It is in 40a88b2902b054e6588b1c0cd7e09533 file.
You can extract mes_all.txt from it and put it back with programs like UnityEX(https://forum.zoneofgames.ru/topic/36240-unityex/) or Asset Bundle Extractor(https://github.com/SeriousCache/UABE/releases).
