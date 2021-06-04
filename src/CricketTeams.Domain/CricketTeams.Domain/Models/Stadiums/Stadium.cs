namespace CricketTeams.Domain.Models.Stadiums
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;
    using static ModelConstants.Stadium;

    public class Stadium : Entity<int>
    {
        internal Stadium(
            string name,
            string address,
            int capacity,
            string websiteUrl,
            string owner)
        {
            Validate(name, address, capacity, websiteUrl, owner);

            this.Name = name;
            this.Address = address;
            this.Capacity = capacity;
            this.WebsiteUrl = websiteUrl;
            this.Owner = owner;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public int Capacity { get; private set; }
        public string WebsiteUrl { get; private set; }
        public string Owner { get; private set; }

        public Stadium UpdateName(string name) 
        {
            ValidateName(name);

            this.Name = name;

            return this;
        }

        public Stadium UpdateAddress(string address) 
        {
            this.Address = address;

            return this;
        }

        public Stadium UpdateCapacity(int capacity) 
        {
            ValidateCapacity(capacity);

            this.Capacity = capacity;

            return this;
        }

        public Stadium UpdateWebsiteUrl(string url)
        {
            ValidateWebsiteUrl(url);

            this.WebsiteUrl = url;

            return this;
        }

        public Stadium UpdateOwner(string ownerName)
        {
            ValidateOwner(ownerName);

            this.Owner = ownerName;

            return this;
        }

        private void Validate(string name, string address, int capacity, string websiteUrl, string owner)
        {
            ValidateName(name);
            //ValidateAddress(address);
            ValidateCapacity(capacity);
            ValidateWebsiteUrl(websiteUrl);
            ValidateOwner(owner);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidStadiumException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

        private void ValidateAddress(string address)
            => throw new InvalidStadiumException("Integrate UPS Api for address validation.");

        private void ValidateCapacity(int capacity)
            => Guard.AgainstOutOfRange<InvalidStadiumException>(
                capacity,
                MinCapacityLength,
                MaxCapacityLength,
                nameof(this.Capacity));

        private void ValidateWebsiteUrl(string websiteUrl)
            => Guard.ForValidUrl<InvalidStadiumException>(
                websiteUrl,
                nameof(this.WebsiteUrl));

        private void ValidateOwner(string owner)
            => Guard.ForStringLength<InvalidStadiumException>(
                owner,
                MinOwnerLength,
                MaxOwnerLength,
                nameof(this.Owner));
    }
}
