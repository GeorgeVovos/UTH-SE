import java.util.List;
import java.util.function.Consumer;

public class StringUpper {
    public static void main(String[] args) {
        // Classic
        String[] words = { "classic", "hello", "world", "java", "programming", "language", "example", "code", "for", "masters", "program" };

        System.out.println("Before:");
        for (String word : words)
            System.out.print(word + " ");

        System.out.println();
        for (int i = 0; i < words.length; i++)
            words[i] = words[i].toUpperCase();

        System.out.println("After:");
        for (String word : words)
            System.out.print(word + " ");

        System.out.println();
        System.out.println();

        // Modernized, comment it out if using an older java version
        Consumer<String> print = word -> System.out.print(word + " ");
        List<String> wordsList = List.of("and", "a", "more", "modern", "version", "which", "uses", "newer", "java", "language", "features");
        wordsList.forEach(print);
        System.out.println();
        wordsList.stream().map(String::toUpperCase).forEach(print);

        System.out.println();
    }
};