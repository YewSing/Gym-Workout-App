# MyWorkoutApp

## Repository layout

- `backend/` lives in this repo (remote: `Gym-Workout-App`) and deploys to Railway, which auto-deploys on push to `main`.
- `frontend/my-workout-app/` is its own separate git repo with its own remote (`Workout-App`), excluded from this repo via `.gitignore`. It ships via EAS — native builds through `eas build`, JS-only changes through `eas update` (OTA).
- Because they're independent repos, commit and push each one separately — a single commit can never span both.

## Working with Claude

- For every code edit or change plan, explain the technical reasoning: what the code currently does, why it needs to change, and what effect the change has on behavior. Don't just present a diff or plan without the technical explanation behind it.
