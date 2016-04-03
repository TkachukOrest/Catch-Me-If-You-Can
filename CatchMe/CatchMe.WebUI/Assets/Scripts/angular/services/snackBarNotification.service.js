(function () {
    angular
      .module('snackBarNotification', [])
      .service('snackBarNotification', snackBarNotificationService);

    snackBarNotificationService.$inject = [];

    function snackBarNotificationService() {       
        //fields
        var previous = null;

        //service
        var service = {
            create: create
        };        
        return service;               

        //functions
        function create(message, actionText, action) {
            if (previous) { previous.dismiss(); }

            var snackbar = document.createElement('div');
            snackbar.classList.add('paper-snackbar');            
            snackbar.classList.add('box-shadow');
            snackbar.dismiss = function() { this.style.opacity = 0; };        
            snackbar.appendChild(document.createTextNode(message));

            if (actionText) {
                if (!action) {
                    action = snackbar.dismiss.bind(snackbar);
                }
                var actionButton = document.createElement('button');
                actionButton.className = 'action';
                actionButton.innerHTML = actionText;
                actionButton.addEventListener('click', action);
                snackbar.appendChild(actionButton);
            }

            setTimeout(function () {
                if (previous === this) {
                    previous.dismiss();
                }
            }.bind(snackbar), 5000);

            snackbar.addEventListener('transitionend', function (event, elapsed) {
                if (event.propertyName === 'opacity' && this.style.opacity == 0) {
                    this.parentElement.removeChild(this);
                    if (previous === this) {
                        previous = null;
                    }
                }
            }.bind(snackbar));


            previous = snackbar;            
            document.body.appendChild(snackbar);

            getComputedStyle(snackbar).bottom;
            snackbar.style.bottom = '0px';
            snackbar.style.opacity = 1;
        };
    };
})();