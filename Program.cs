//*****************************************************************************
//** 664. Strange Printer   leetcode                                         **
//** Recursive function using a hash table to find the minimum number of     **
//** turns to change the letters on the "strange printer"    -Dan            **
//*****************************************************************************

int printed[101][101]; // Assuming the string length is maximum 100.

int turn(char* s, int i, int j) {
    if (i > j) return 0; // Empty substring.
    if (printed[i][j] > 0) return printed[i][j]; // Cached result.

    // Fall back: initialize with the result of printing s[i:j-1] plus one additional turn for s[j].
    printed[i][j] = turn(s, i, j - 1) + 1;

    // Check for matching characters within the range to reduce turns.
    for (int k = i; k < j; k++) {
        if (s[j - 1] == s[k - 1]) {
            printed[i][j] = printed[i][j] < (turn(s, i, k) + turn(s, k + 1, j - 1)) ? printed[i][j] : (turn(s, i, k) + turn(s, k + 1, j - 1));
        }
    }

    return printed[i][j];
}

int strangePrinter(char* s) {
    int len = strlen(s);
    memset(printed, 0, sizeof(printed)); // Initialize dp array with 0s.
    return turn(s, 1, len); // Start the recursive function from indices 1 to len.
}

/*
//*******************************************************************
//** deprecated code, first attempt.                               **
//*******************************************************************

int strangePrinter(char* s) {
    int len = strlen(s);
    if(len == 0) return 0;

    int printed[len][len];
    int min = INT_MAX;
    int max = INT_MIN;
    int count = 0;
    int lastConvert = 0;
    for(int i = 0; i < len; i++)
    {
        printed[i][i] = 1;
    }

    for(int j = 2; j <= len; j++)
    {
        for(int k = 0; k <= len - j; k++)
        {
            int l = k + j - 1;
            printed[k][l] = printed[k][l - 1] + 1;
            for(int m = k; m < l; m++)
            {
                if(s[m] == s[l])
                {
//                    printed[k][l] = printed[k][m] + (m + 1 <= l - 1 ? printed[m + 1][l - 1] : 0);
                    printed[k][l] = printed[k][m] + (m + 1 <= l - 1 ? printed[m + 1][l - 1] : 0);
//                    printed[k][l] = printed[k][l] < printed[k][l - 1] ? printed[k][l] : printed[k][l - 1];
                }
//
//                else
//                {
//                    printed[k][l] = printed[k][l] < printed[k][m] ? printed[k][l] : printed[k][m];
////                    printed[k][l] = printed[k][l] < printed[k][l - 1] ? printed[k][l - 1] : printed[k][l];
//                }
                printed[k][l] = printed[k][l] < printed[k][m] + printed[m + 1][l] ? printed[k][l] : printed[k][m] + printed[m + 1][l];
            }
        }
    }
    return printed[0][len - 1];
}
*/
