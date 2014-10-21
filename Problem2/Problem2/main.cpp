#include <iostream>
#include <vector>
using namespace std;
/*
Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:

1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...

By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
*/

//using dynamic programming.
int find_sum_even_fib(vector<int> & f)
{
	int sum = 0;
	int n = f.size();
	int fib = 0;
	while(fib < 4000000)
	{
		fib = f[n-1] + f[n-2];
		f.push_back(fib);
		n++;
		if(fib % 2 == 0)
			sum += fib;
	}
	return sum;
}

//a little better implementation.
int find_sum_even_fib_without_space_allocation()
{
	int sum = 0;
	int a = 1;
	int b = 1;
	int fib = 0;
	while(fib < 4000000)
	{
		fib = a + b;
		a = b;
		b = fib;
		if(fib % 2 == 0)
			sum += fib;
	}
	return sum;
}

int main()
{
	vector<int> fib;
	fib.push_back(1);
	fib.push_back(1);
	cout << find_sum_even_fib(fib) << endl;
	cout << find_sum_even_fib_without_space_allocation() << endl;
	return 0;
}