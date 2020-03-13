using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class fbnaci : MonoBehaviour
{
    int Fbnachi(int n)
    {
        if (n == 1 || n == 2)
        {
            return 1;
        }
        int f1 = 0;
        int f2 = 1;
        int c = 0;
        for (int i = 3; i <= n; i++)
        {
            c = f1 + f2;
            f1 = f2;
            f2 = c;
        }
        return c;
    }
    int Fbnachi_Recursive(int n)
    {
        if (n == 1 || n == 2) return 1;
        return Fbnachi_Recursive(n - 1) + Fbnachi_Recursive(n - 2);
    }
}
