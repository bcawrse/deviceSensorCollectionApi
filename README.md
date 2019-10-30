# deviceSensorCollectionApi

Solution for registering devices and then accepting sensor data from those devices.

## Solution Details

This solution contains two dotnet core projects, *DeviceSensorApi* and *DeviceSensorWeb*. DeviceSensorApi is the HTTP API for devices to connect to, register with, and submit their sensor information. Authentication to the API is managed with BASIC Authentication. DeviceSensorWeb is a portal to view the submitted information collected by the HTTP API. Authentication is managed with Bearer Authentication and Json Web Tokens for the web portal. Both projects use hard-coded users but the authentication in each is still functioning.

## Considerations for the Future

A few things I would like to have gotten to.

### Stretch Goals

With more time I would have implemented more stretch goals.

* After the asynchronous data updating was enabled for the web portal, the plan was to add a notification system to alert users when particular sensor health readings were submitted. This was a low-effort, moderate value target so higher in priority than other targets.
* Admin management pages were high up on my list because they could add a large amount of perceived value with low effort; as long as the features were to be partially faked as planned. This would have helped the portal to feel less empty.
* I wanted to style the portal a bit more and make it feel more personal, give it more character. My initial designs included the client logo to brand the prototype and provide the client with a sense of ownership, strengthen the relationship that they're getting something for their money. The only branding I managed was to style the header row of the Devices table with a color from the client site which appeared to be part of their branding colors.
* Another round of cleaning up the graphs would have been nice. I swapped the default translucent, grey with a blue but it didn't fit in and removed information since it was opauqe; I shelved the color updates as a quick win if time allowed.
* I had plans to hide a separate set of data behind different account credentials. The second set would have options to constantly create data at different intervals, wipe all data, and bulk create data. This would have better displayed the capabilities of the web portal.

### Code Improvemnts

Changes I would have made with the project design and architecture given more time.

* The DB connection would have been better managed so the web app either retrieved data from the HTTP API or connected with a readonly user. I'm not a fan of multiple sources with access to add directly to a database, unless it fits the design. Since the DB logic was already worked out, it was a quick time-win to re-use it.
* I began separating the javascript ajax logic into a service behind an old namespacing technique of setting an IFFE to a window scoped variable, but abandoned it after it felt like I could shave time without it. Works well to inject namespaces into other IFFE's; some of this code was left in. The plan was to have a client-side service act as a gateway to server queries so it'd be easier to keep the app from getting too chatty as it expanded. It would also help consolidate logic and keep it from sprawling throughout the app. Not necessary for a prototype necessarily, but I would have felt better about the code.
* Fixed my javascript minifier, watcher. After I realized it was broken, I attempted quick fixes then pressed on with my npm build:js script. It was missed.
* Test plans! It would have been great to have implemented a test plan in the API and portions of the web portal.
* CI/CD pipeline! With enough additional time I would have setup a simple CI/CD workflow for rapid deployments. Probably just git hooks for my Linode. Not so critical here but very useful in quick-feedback-turnaround scenarios.
* Generally cleaner code. As a prototype, it needs to be quick now not manageable later.
* Quick web wins of better handling and bundling of resources.

## fin

This was a fun experience and a wonderful whirlwind through dotnet core. My normal stack is Windows dependent and I'm not a big fan of getting tied up with Windows hosting for personal projects. It appeared to me to be an easier, more fun, lift of picking up dotnet core and hosting on my Linode than to get cheap, full-access, to a public Windows server. I've worked with other languages and stacks but since I'm most familiar with .Net I didn't want to tread too far. I was able to pull a little bit of Vue into the mix, and that's always fun.
