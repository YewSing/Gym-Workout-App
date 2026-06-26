# MyWorkoutApp – TODO

## Workout Plan Page

- [ ] **1.** Add pagination button to chart by time (this week, last week)
- [ ] **2.** Use another color for the chart bars
- [ ] **3.** Tapping on the chart bar should show details in a small label e.g. 4600kg 1h 32m 21/06

## Session Details Page — Analytics

- [ ] **1.** Under the three summary cards, add an analytics row based on the last session of the same workout plan same gym. (e.g. "15% higher training volume than the last session!" for "chest day" workout plan at gym A, compared to the last session for the same workout plan at the same gym ONLY. )
- [ ] **2.** For each exercise, add an analytics label on the same row as the exercise name (e.g. a profit/gain icon + "3%")
  - Yellow label with stripe background for improved volume
  - Gray label for same or declined volume
  - Special highlighted label for new PR sets — placed top-right of the row, cheerful icon (slanted trophy, ticket, etc.)

## Exercise Page / Tab

- [ ] **1.** Add more labels — change muscle group text to colored labels, possibly with icon or small image

## Exercise Details Page

- [ ] **1.** Adjust the padding of the top card (Personal Record — xx kg × x reps)
- [ ] **2a.** Increase the padding between the session history data cards
- [ ] **2b.** Add ">" button for navigation to that session
- [ ] **3.** Add PR record label
- [ ] **4.** Adjust the training volume and "Volume" position
- [ ] **5.** Add pagination button to chart by time (this week, last week), change the chart data to training volume, not max weight
- [ ] **5.** Reverse the position of the PR card and the chart
  

## Authorization / Authentication

- [ ] **1.** Extend access token

## Deployment

- [ ] Push backend to GitHub
- [ ] Deploy backend + PostgreSQL on Railway (free tier)
- [ ] Update app's API base URL to point to the deployed Railway URL
- [ ] Build Android APK via EAS (`eas build --platform android --profile preview`) and share directly with the 5 users
- [ ] Use `eas update` for OTA updates after backend/app changes
