export enum DestinationType {
  COUNTRY = 'Country',
  CITY = 'City',
}

export enum DesType {
  Country = 0,
  City = 1,
}

export enum Role {
  ADMIN = 'Admin',
  TRAVELAGENCY = 'TravelAgency',
  TOURGUIDE = 'TourGuide',
}

export enum Currency {
  USD = 0,
  VND = 1,
}

export enum LanguageSpoken {
  English = 0,
  Vietnamese = 1,
}

export enum ApplicationStatus {
  Pending = 'Pending',
  Canceled = 'Canceled',
  Rejected = 'Rejected',
  Accepted = 'Accepted',
}

export enum ApplicationTypes {
  New,
  Cancel,
  ReApply,
}

export enum TourJobStatus {
  Live = 'Live',
  Finished = 'Finished',
  Expired = 'Expired',
}
