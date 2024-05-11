import { InterviewerModel,JobModel } from "./index";

export interface CandidateModel {
    id: number,
    firstName: string,
    lastName: string,
    phoneNumber: string,
    email: string,
    createDate: string,
    jobModels: Array<JobModel>,
    interviewerModels : Array<InterviewerModel>
}