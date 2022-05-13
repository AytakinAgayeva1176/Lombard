window.addEventListener('load', function () {
    setTimeout(UpdateReturnDate, 400);

    function UpdateReturnDate() {
        console.clear();

        var giving_date_element_array = document.querySelectorAll(".giving_date");

        for (i = 0; i < giving_date_element_array.length; i++) {

            var giving_date_element = giving_date_element_array[i];
            var giving_date_string = giving_date_element_array[i].textContent;
            var giving_date_value = new Date(giving_date_string);

            var closest_table_row = giving_date_element.closest(".table-row-inside-table");
            var closest_return_date_element = closest_table_row.querySelector(".return_date");
            var closest_credit_period = closest_table_row.querySelector(".credit_period").textContent;

            giving_date_value = giving_date_value.toLocaleString('en-US', {
                day: 'numeric', // numeric, 2-digit
                year: 'numeric', // numeric, 2-digit
                month: 'numeric', // numeric, 2-digit, long, short, narrow
            });

            var return_date_value = new Date(giving_date_string);
            return_date_value.setMonth(return_date_value.getMonth() + parseInt(closest_credit_period));
            return_date_value = return_date_value.toLocaleString('en-US', {
                day: 'numeric', // numeric, 2-digit
                year: 'numeric', // numeric, 2-digit
                month: 'numeric', // numeric, 2-digit, long, short, narrow
            });

            closest_return_date_element.textContent = return_date_value;
        }
    }
});
