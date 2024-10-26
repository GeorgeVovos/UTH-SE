
import java.util.Arrays;

public class SortArray {
    public static void main(String[] args) {
        int[] numbers = {43, 12, 89, 5, 77, 30, 50, 19, 8, 62};
        System.out.println("Original Array: " + Arrays.toString(numbers));
        bubbleSort(numbers);
        System.out.println("Sorted Array: " + Arrays.toString(numbers));

        System.out.println();
    }

    private static void bubbleSort(int[] array) {
        boolean swapped;

        for (int i = 0; i < array.length - 1; i++) {
            swapped = false;
            for (int j = 0; j < array.length - i - 1; j++) {
                if (array[j] > array[j + 1]) {
                    int temp = array[j + 1];
                    array[j + 1]=array[j];
                    array[j] =temp;
                    swapped = true;
                }
            }

            if (!swapped) break;
        }
    }
};