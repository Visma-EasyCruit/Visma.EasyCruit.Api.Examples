# Examples for Visma EasyCruit SOAP API
Currently, there is a single Postman collection that demonstrates how to use the SOAP API
 to perform the vacancy application process - [Visma EasyCruit SOAP API (application form)](Visma%20EasyCruit%20SOAP%20API%20%28application%20form%29.postman_collection.json).
## Visma EasyCruit SOAP API (application form)
### Setup
1. Import the Postman collection into your Postman application
2. Set the environment variables in Postman:
   - **customer** - The customer URI identifier in Visma Easycruit.
   - **username** - The username of the API user in Visma Easycruit.
   - **password** - The password for the API user in Visma Easycruit.
   - **domain** - The domain to use the collection on. This usually should not be changed and should stay as easycruit.com.
   - **miscQuestionRadioButtonId** - The ID of the radio button question in the candidate profile form.
   - **miscQuestionRadioButtonAnswerId** - The ID of the answer to the radio button question in the candidate profile form.
   - **miscQuestionCheckboxId** - The ID of the checkbox question in the candidate profile form.
   - **miscQuestionCheckboxFirstSelection** - The ID of the first selection in the checkbox question in the candidate profile form.
   - **miscQuestionCheckboxSecondSelection** - The ID of the second selection in the checkbox question in the candidate profile form.
   - **miscQuestionTextId** - The ID of the text question in the candidate profile form.
   - **miscQuestionHrClassificationId** - The ID of the HR classification question for the candidate profile.
   - **vacancyId** - The ID of the vacancy to which the candidate is applying.
   - **departmentId** - The ID of the department to which the candidate is applying.
   - **recruiterId** - The ID of the recruiter who should be set as the one who has created the application.
   - **applicationMiscQuestionTextId** - The ID of the text question in the application form.
   - **applicationMiscQuestionRadioButtonId** - The ID of the radio button question in the application form.
   - **applicationMiscQuestionRadioButtonAnswerId** - The ID of the answer to the radio button question in the application form.
   - **lastCandidateId** - the ID of the last created candidate via the **Add candidate** request. This is used for all the later requests and should not be set or changed.
   - **miscQuestionUploadId** - The ID of the upload question in the candidate profile form.
   - **applicationMiscQuestionUploadId** - The ID of the upload question in the application form.
![Postman collection setup](/docs/assets/soap_application_env_setup.png)

3. Make sure that you use the correct values for some default questions, like personality, language qualification,
work experience, education, etc. These values are not set in the environment variables and should be set to the correct values
for the requests itself. You can find the correct values in the **Get Customer fields** request.


### Content
The collection contains the following requests:
- **Get Customer fields** - 
  This request retrieves the predefined customer fields for a specific customer and language.
- **Add candidate** - 
  This request adds a new candidate to the system with the provided candidate details. **Please note** it simply adds the candidate
  to the candidate pool, not adds them to a specific vacancy.
- **Attach CV** - 
  This request attaches a CV to the newly created candidate. The CV file is provided as a base64-encoded string.
- **Attach profile picture** - 
  This request attaches a profile picture to the newly created candidate. The profile picture file is provided as a base64-encoded string.
- **Attach Work Experience document** - 
  This request attaches a work experience document to the newly created candidate. The document file is provided as a base64-encoded string.
  **Please note** to select the work experience item, from the **Add candidate** request you must
  set the correct ``SequenceNumber`` value in the request body. ``1`` means that the document will be
  attached to the first work experience item, ``2`` means that the document will be attached to the second work experience item, and so on.
- **Attach Education document** - 
  This request attaches an education document to the newly created candidate. The document file is provided as a base64-encoded string.
  **Please note** to select the education item, from the **Add candidate** request you must
  set the correct ``SequenceNumber`` value in the request body. ``1`` means that the document will be
  attached to the first education item, ``2`` means that the document will be attached to the second education item, and so on.
- **Attach HR document** - 
  This request attaches an HR document to the newly created candidate. The document file is provided as a base64-encoded string.
- **Attach Misc candidate file** - 
  This request attaches a miscellaneous candidate file to the newly created candidate. The document file is provided as a base64-encoded string.
  For this request make sure that you provided the correct ```CustomFieldId```, ```CustomFieldType``` value should be set to ```cv```.
- **Add candidate to the vacancy (application)** - 
  This request adds the candidate to the vacancy. The request body contains the candidate ID and the vacancy ID and department ID.
  **Please note** this request actually adds the candidate to the vacancy and sends the application. After this
  you can see the new application under the vacancy. Also within this request it is possible to pass the cover letter
  in the text format and also the answers to non-upload miscellaneous application questions.
- **Attach Cover Letter document** - 
  This request attaches a cover letter document to the newly created application. The document file is provided as a base64-encoded string.
  **Please note** you will need to provide the correct ```RecruitingProjectId``` and ```DepartmentId``` in this request. ```CustomFieldType``` value
  should be always set to ```application```.
- **Attach Misc application file** - 
  This request attaches a miscellaneous application file to the newly created application. The document file is provided as a base64-encoded string.
  **Please note** you will need to provide the correct ```RecruitingProjectId``` and ```DepartmentId``` in this request. ```CustomFieldType``` value
  should be always set to ```misc_application```. For this request make sure that you provided the correct ```CustomFieldId```.
- **Candidate export** - 
  This request retrieves the candidate data for a snewly created candidate. This request is mainly just to test
  that candidate was added correctly.