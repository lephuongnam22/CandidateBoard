import { CandidateModel } from "./index";

export interface CandidateStautusModel {
   status: string,
   candidates: CandidateModel[],
   dropStatus: string []
}