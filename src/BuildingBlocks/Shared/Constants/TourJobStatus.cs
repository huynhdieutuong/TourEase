namespace BuildingBlocks.Shared.Constants;
public enum TourJobStatus
{
    Live,
    Finished, // If Agency click Choose, update TourGuide, Status to Finished
    Expired // If Agency not click Choose, TourGuide null, ExpiredDate < Now => Change Status to Expire in Background job
}
