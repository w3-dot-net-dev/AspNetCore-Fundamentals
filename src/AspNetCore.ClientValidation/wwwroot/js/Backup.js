$(function() {
    var $inputs = $('input[data-val="true"]'),
        $forms = $(document.forms[0]),
        $btnSubmit = $forms.find(':submit');

    function validateAllInputs(excludeElement) {
        var isValid;

        $inputs.each(function() {
            isValid = this === excludeElement || $(this).valid();
            return isValid;
        });

        return isValid;
    };

    function remoteComplete(xhr, textStatus) {
    };

    $inputs.each(function() {
        var $currentElement = $(this);

        //$currentElement.valid();

        if ($currentElement.data('val-remote-url')) {
            $currentElement.rules().remote.complete = remoteComplete;
        }

        $currentElement.keyup(function() {
            //var isValid = $currentElement.valid() && validateAllInputs(this);

            var $this = $(this),
                validator = $(this.form).validate(),
                valid,
                errorList;

            if ($this.is("form")) {
                valid = validator.form();
            } else {
                errorList = [];
                valid = true;
                $this.each(function() {
                    valid = validator.element($this) && valid;
                    errorList = errorList.concat(validator.errorList);
                });
                validator.errorList = errorList;
            }

            $btnSubmit.prop('hidden', !valid);
        });
    });

});