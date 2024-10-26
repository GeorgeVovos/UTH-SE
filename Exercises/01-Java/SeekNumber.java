import java.util.Arrays;
import java.util.Random;

public class SeekNumber {
    public static void main(String[] args) {
        int[] numbers = { 43, 12, 89, 5, 77, 30, 50, 19, 8, 62 };
        Arrays.sort(numbers);

        int numberToFind = new Random().nextInt(101);
        var numberIndex = myBinarySearch(numbers, numberToFind);
        System.out.println(numberIndex < 0 ? "Number " + numberToFind + " not found" : "Number " + numberToFind + " found at index "+ numberIndex  + " of the sorted array" );
    }

    private static int myBinarySearch(int[] array, int numberToFind) {
        // Built-in method
        // return Arrays.binarySearch(array, numberToFind);

        int left = 0;
        int right = array.length - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;

            if (array[mid] == numberToFind) {
                return mid;
            }

            if (array[mid] < numberToFind) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }

        return -1;
    }
}
