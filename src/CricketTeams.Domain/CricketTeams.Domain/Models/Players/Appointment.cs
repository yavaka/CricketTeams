namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using System;
    using static ModelConstants.Common;

    public class Appointment : ValueObject
    {
        internal Appointment(
            string description,
            string service,
            string appointmentWith,
            DateTime startDate,
            DateTime endDate)
        {
            Validate(description, service, appointmentWith);

            this.Description = description;
            this.Service = service;
            this.AppointmentWith = appointmentWith;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        private Appointment(
            string description,
            string service,
            string appointmentWith)
        {
            this.Description = description;
            this.Service = service;
            this.AppointmentWith = appointmentWith;

            this.StartDate = default!;
            this.EndDate = default!;
        }

        public string Description { get; private set; }
        public string Service { get; private set; }
        public string AppointmentWith { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public TimeSpan? Duration => StartDate - EndDate;

        private void Validate(string description, string service, string appointmentWith)
        {
            ValidateDescription(description);
            //ValidateService(service);
            //ValidateAppointmentWith(appointmentWith);
        }

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidAppointmentException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
    }
}