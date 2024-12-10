import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.stream.Collectors;

public class Day5 {
    public static void main(String[] args) {
        try {
            final long startTime = System.currentTimeMillis();
            String content = new String(Files.readAllBytes(Paths.get("./input.txt")), "UTF-8");
            String[] splitArr = content.split("\\n\\r\\n");
            List<String> orderPairs = Arrays.asList(splitArr[0].split("\n")).stream().map(i -> i.trim()).collect(Collectors.toList());
            Map<String, List<String>> rules = generateRuleMapping(orderPairs);
            List<String> instructions = Arrays.asList(splitArr[1].split("\n")).stream().map(i -> i.trim()).collect(Collectors.toList());
            int day1Ans = Part1(rules, instructions);
            int day2Ans = Part2(rules, instructions);
            System.out.println(String.format("Part 1: %d\nPart 2: %d", day1Ans, day2Ans));
            final long endTime = System.currentTimeMillis();
            System.out.println(String.format("Runtime: %dms", endTime - startTime));
        } catch(IOException e) {
            
        }
    }

    private static Map<String, List<String>> generateRuleMapping(List<String> orderPairs) {
        Map<String, List<String>> rules = new HashMap<String, List<String>>();
        for (String orderPair : orderPairs) {
            String[] pair = orderPair.split("\\|");
            List<String> ruleList;
            if (rules.containsKey(pair[0])) {
                ruleList = rules.get(pair[0]);
            } else {
                ruleList = new ArrayList<>();
            }
            ruleList.add(pair[1].trim());
            rules.put(pair[0], ruleList);
        }
        return rules;
    }

    public static int Part1(Map<String, List<String>> rules, List<String> instructions) {
        int score = 0;
        for (String instruction : instructions) {
            String[] pages = instruction.split(",");
            int i = 0;
            boolean isValid = true;
            Set<String> seen = new HashSet<>();
            while (isValid && i < pages.length) {
                String page = pages[i];
                List<String> pageRules = rules.get(page);
                if (pageRules != null) {
                    for (String prev : seen) {
                        if (pageRules.contains(prev)) {
                            isValid = false;
                        }
                    }
                }
                seen.add(page.trim());
                i++;
            }
            if (isValid) score += Integer.valueOf(pages[pages.length / 2]);
        }
        return score;
    }

    public static int Part2(Map<String, List<String>> rules, List<String> instructions) {
        List<List<String>> fixed = new ArrayList<>();
        for (String instruction : instructions) {
            String[] pages = instruction.split(",");
            List<String> adjusted = new ArrayList<String>(Arrays.asList(pages));
            boolean isValid = true;
            Set<String> seen = new HashSet<>();
            for (int i = 0; i < pages.length; i++) {
                String page = pages[i];
                List<String> pageRules = rules.get(page);
                if (pageRules != null) {
                    int earliestIndex = Integer.MAX_VALUE;
                    for (String prev : seen) {
                        if (pageRules.contains(prev)) {
                            if (isValid) isValid = false;
                            earliestIndex = earliestIndex < adjusted.indexOf(prev) ? earliestIndex : adjusted.indexOf(prev);
                        }
                    }
                    if (earliestIndex < Integer.MAX_VALUE) {
                        adjusted.add(earliestIndex, page);
                        adjusted.remove(i + 1);
                    }
                }
                seen.add(page.trim());
            }
            
            if (!isValid) fixed.add(adjusted);
        }
        return fixed.stream().mapToInt(str -> Integer.valueOf(str.get(str.size() / 2))).reduce(0, (subtotal, element) -> subtotal + element);
    }
}