using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : MonoBehaviour
{
    public static int[] RandomCreate(int count, int RangeMin, int RangeMax)
    {
        int[] nums = new int[count];
        //1. �ŰԺ������� ���� count ������ ���� ������ ���� �迭�� ����ϴ�.

        List<int> rangenums = new List<int>();
        // 2. �׸���  �� �ٸ� ����Ʈ�� �ϳ� ����ϴ�. 

        for (int i = 0; i < RangeMax - RangeMin; i++)
        {
            rangenums.Add(i + RangeMin);// rangenums[0] = 0+RangeMin �̶�� ���ڰ� ���ϴ�.
        }
        //3. ������ ���� ����Ʈ�� RangeMin���� RangeMax������ ������ �߰��մϴ�.

        for (int i = 0; i < count; i++)
        {
            int RandomNum = Random.Range(0, rangenums.Count);
            // 4.RandomNum ������ ������ ���� ������ ������ ���� �ϳ� �����մϴ�.
            nums[i] = rangenums[RandomNum - RangeMin];
            // 5. �׸��� rangenums����Ʈ�� ����� ������ ������ �ּҰ��� �� ����°�� ���� ó�� ���� �迭 nums�� �߰��մϴ�.
            rangenums.RemoveAt(RandomNum - RangeMin);
            // 6. �׸��� �ѹ� �� ���� rangenums ����Ʈ���� ���� �˴ϴ�. �׷��Ƿ� �ѹ� ���� ���� ����Ǵ� ���� �������ϴ�.

        }
        // 7.�׷��� ���� ���� ���� ��ŭ nums[] �迭�� ����ݴϴ�.

        return nums; // 8.nums ��� �迭�� ��ȯ�մϴ�.  

    }
}
