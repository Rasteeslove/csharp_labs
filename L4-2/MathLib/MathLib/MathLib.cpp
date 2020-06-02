#include "pch.h"
#include <utility>
#include <limits.h>
#include "MathLib.h"

int Add(int a, int b)
{
	return a + b;
}

int Subtract(int a, int b)
{
	return a - b;
}

int Multiply(int a, int b)
{
	return a * b;
}

float Divide(float a, float b)
{
	return a / b;
}

int Power(int a, int b)
{
	if (b <= 0) return 1;
	else return a * Power(a, b - 1);
}

int Factorial(int a)
{
	if (a <= 0 || a > 12) return 1;
	else return a * Factorial(a - 1);
}

int GCD(int a, int b)
{
	if (a == 0 && b != 0)
		return b;
	else if (a != 0 && b == 0)
		return a;
	else if (a == 0 && b == 0)
	{
		// undefined but let's return 1
		return 1;
	}

	a = a < 0 ? -a : a;
	b = b < 0 ? -b : b;

	while (a != b)	
		if (a > b)
			a -= b;
		else
			b -= a;
	
	return a;
}

int LCM(int a, int b)
{
	return (a * b) / GCD(a, b);
}