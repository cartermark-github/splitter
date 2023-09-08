# splitter
Split/Merge large files so they can be saved to DVD.

Splitter has two commands SPLIT and MERGE.

Using SPLITTER SPLIT.

SPLITTER SPLIT [PATH][FILENAME] [Number of Splits]

Example:

SPLITTER SPLIT “C:\USERS\ISO\BIGFILE.ISO” 4

This will split the file BIGFILE.ISO into 4 parts.

NOTE: Always include the full path to the file.

NOTE: File parts must be 2GB or smaller.  If you get an error, increase the number of splits until they are below 2GB.  This can be calculated by dividing the file size by 2GB.  So, if the file is 10GB, it will need 5 splits.

Using SPLITTER MERGE.

SPLITTER MERGE [PATH][FOLDER NAME]

Example:

SPLITTER MERGE “C:\USERS\DESKTOP\TEST”

This will merge all the files in the TEST folder.

NOTE: Always include the full path to the folder.
