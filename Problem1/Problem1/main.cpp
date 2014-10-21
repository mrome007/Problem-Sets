#include <iostream>
#include <vector>
using namespace std;
/*

If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.

Find the sum of all the multiples of 3 or 5 below 1000.

*/
void find_multiples_3_5(vector<int> & m)
{
	
	for(int i = 1; i < 1000; i++)
	{
		if(i % 3 == 0 || i % 5 == 0)
			m.push_back(i);
	}
}

int sum_of_multiples(vector<int> m)
{
	int sum = 0;
	for(int i = 0; i < m.size(); i++)
		sum += m[i];
	return sum;
}

int efficient_multiples_3_5()
{
	int sum3 = 0;
	int sum5 = 0;
	int sum15 = 0;
	for(int i = 3; i < 1000; i += 3)
		sum3 += i;
	for(int i = 5; i < 1000; i += 5)
		sum5 += i;
	for(int i = 15; i < 1000; i += 15)
		sum15 += i;

	return sum3 + sum5 - sum15;
}

int main()
{
	vector<int>multiples;
	
	find_multiples_3_5(multiples);
	cout << sum_of_multiples(multiples) << endl;
	cout << efficient_multiples_3_5() << endl;



	return 0;
}