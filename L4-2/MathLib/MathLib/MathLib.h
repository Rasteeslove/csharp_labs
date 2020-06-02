#pragma once

#ifdef MATHLIBRARY_EXPORTS
#define MATHLIBRARY_API __declspec(dllexport)
#else
#define MATHLIBRARY_API __declspec(dllimport)
#endif

extern "C" MATHLIBRARY_API int Add(int a, int b);
extern "C" MATHLIBRARY_API int Subtract(int a, int b);
extern "C" MATHLIBRARY_API int Multiply(int a, int b);
extern "C" MATHLIBRARY_API float Divide(float a, float b);
extern "C" MATHLIBRARY_API int Power(int a, int b);
extern "C" MATHLIBRARY_API int Factorial(int a);
extern "C" MATHLIBRARY_API int GCD(int a, int b);
extern "C" MATHLIBRARY_API int LCM(int a, int b);