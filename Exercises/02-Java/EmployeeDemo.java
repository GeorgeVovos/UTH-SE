import java.util.ArrayList;
import java.util.List;

public class EmployeeDemo {
    public static void main(String[] args) {

        List<Employee> employees = new ArrayList<>();
        employees.add(new SalariedEmployee("George Vovos", "AMF42", 1200));
        employees.add(new HourlyEmployee("George Kakarontzas", "AMF123", 6, 180));

        for (Employee employee : employees) {
            System.out.println("Employee Type: " + employee.getClass().getSimpleName());
            System.out.println("Name: " + employee.getName());
            System.out.println("Tax Code: " + employee.getTaxCode());
            System.out.println("Payment: " + employee.payment());
            System.out.println();
        }
    }
};

abstract class Employee {
    // The exercise asks for the fields to be protected but they can be private
    // since subclasses don't need direct access
    protected String name;
    protected String taxCode;

    public Employee(String name, String taxCode) {
        this.name = name;
        this.taxCode = taxCode;
    }

    public Employee() {
    }

    public String getName() {
        return name;
    }

    public String getTaxCode() {
        return taxCode;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setTaxCode(String taxCode) {
        this.taxCode = taxCode;
    }

    public abstract int payment();
}

class SalariedEmployee extends Employee {
    private int salary;

    public SalariedEmployee(String name, String taxCode, int salary) {
        super(name, taxCode);
        this.salary = salary;
    }

    public SalariedEmployee() {
    }

    public int getSalary() {
        return salary;
    }

    public void setSalary(int salary) {
        this.salary = salary;
    }

    @Override
    public int payment() {
        return salary;
    }
}

class HourlyEmployee extends Employee {
    private int hourlyPayment;
    private int hoursWorked;

    public HourlyEmployee(String name, String taxCode, int hourlyPayment, int hoursWorked) {
        super(name, taxCode);
        this.hourlyPayment = hourlyPayment;
        this.hoursWorked = hoursWorked;
    }

    public HourlyEmployee() {
    }

    public int getHourlyPayment() {
        return hourlyPayment;
    }

    public void setHourlyPayment(int hourlyPayment) {
        this.hourlyPayment = hourlyPayment;
    }

    public int getHoursWorked() {
        return hoursWorked;
    }

    public void setHoursWorked(int hoursWorked) {
        this.hoursWorked = hoursWorked;
    }

    @Override
    public int payment() {
        return hourlyPayment * hoursWorked;
    }
}
